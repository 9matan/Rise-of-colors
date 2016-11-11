using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RoC;
using UniRx;



namespace RoC
{

	public enum ERoCColor
	{
		NONE,
		RED,
		GREEN,
		BLUE,
		YELLOW,
		WHITE,
		BLACK
	}

	public class Position2D
	{
		public int row;
		public int column;

		public Position2D(int __row, int __column)
		{
			row = __row;
			column = __column;
		}

		public override int GetHashCode()
		{
			return row + 10000 * column;
		}

		public static bool operator ==(Position2D p1, Position2D p2)
		{
			return p1.row == p2.row && p1.column == p2.column;
		}

		public static bool operator !=(Position2D p1, Position2D p2)
		{
			return p1.row != p2.row || p1.column != p2.column;
		}

		public override bool Equals(object obj)
		{
			return (Position2D)(obj) == this;
		}
	}

	[System.Serializable]
	public class RoCColorGroup
	{

		/// <summary>
		/// Average column, row
		/// </summary>
		public Position2D averagePosition
		{
			get { return GetAveragePos(); }
		}
		public int number
		{
			get { return _positions.Count; }
		}

		[SerializeField]
		protected ERoCColor _color;
		[SerializeField]
		protected List<Position2D> _positions;

		public RoCColorGroup(ERoCColor __color)
		{
			_color = __color;
		}

		public Position2D GetPosition(int index)
		{
			return _positions[index];
		}

		public Position2D GetAveragePos()
		{
			int r = 0, c = 0;

			for (int i = 0; i < _positions.Count; ++i)
			{
				r += _positions[i].row;
				c += _positions[i].column;
			}

			return new Position2D(r / number, c / number);
		}

		public void AddPosition(Position2D pos)
		{
			_positions.Add(pos);
		}
	}

	public class RoCColorRecognation : MonoBehaviour,
		IVOSBuilder
	{

		public RoCColorsManager colorsManager
		{
			get { return RoCDirecrot.colorsManager; }
		}

		public int mapRows
		{
			get { return map.GetLength(0); }
		}

		public int mapColumns
		{
			get { return map.GetLength(1); }
		}


		public RGB[,] map
		{
			get; protected set;
		}


		//
		// < DSU >
		//

		public class DSUNode
		{
			public DSUNode parent;
			public Position2D pos;
			public ERoCColor color;

			public DSUNode(Position2D __pos, ERoCColor __color)
			{
				parent = this;
				pos = __pos;
				color = __color;
			}

			public DSUNode GetParent()
			{
				return _GetParent(parent);
			}

			protected DSUNode _GetParent(DSUNode node)
			{
				if (node.pos == pos) return node;
				return parent = _GetParent(parent);
			}

			public bool IsOneUnion(DSUNode node)
			{
				return node.GetParent().pos == GetParent().pos;
			}

			public void Unite(DSUNode node)
			{
				GetParent().parent = node.GetParent();
			}
		}

		protected DSUNode[,] _dsu;

		protected List<Position2D> _dsuMaks = new List<Position2D>()
		{
			new Position2D(-1, -1), new Position2D(-1, 0), new Position2D(-1, 1),
			new Position2D(0, -1), new Position2D(0, 1),
			new Position2D(1, -1), new Position2D(1, 0), new Position2D(1, 1)
		};

		protected void _InitializeDSU(int rows, int columns)
		{
			_dsu = new DSUNode[rows, columns];

			for (int i = 0; i < _dsu.GetLength(0); ++i)
			{
				for (int j = 0; j < _dsu.GetLength(1); ++j)
				{
					_dsu[i, j] = new DSUNode(
						new Position2D(i, j),
						_GetColor(i, j));
				}
			}
		}

		protected bool _IsValid(int row, int column)
		{
			return row >= 0 && row < mapRows && column >= 0 && column < mapColumns;
		}

		// < Union >

		protected void _UniteAll()
		{
			for (int i = 0; i < _dsu.GetLength(0); ++i)
			{
				for (int j = 0; j < _dsu.GetLength(1); ++j)
				{
					_dsu[i, j].GetParent();
				}
			}
		}

		protected void _Unite(int row, int column)
		{
			int nr, nc;
			var node = _dsu[row, column];

			for (int i = 0; i < _dsuMaks.Count; ++i)
			{
				nr = row + _dsuMaks[i].row;
				nc = column + _dsuMaks[i].column;
				var n = _dsu[nr, nc];

				if (_IsValid(nr, nc) && _IsSameColor(node, n))
					node.Unite(n);
			}
		}

		protected bool _IsSameColor(DSUNode node, DSUNode n)
		{
			return node.color == n.color;
		}

		// </ Union >


		protected void _RecalcDSUParents()
		{
			for (int i = 0; i < _dsu.GetLength(0); ++i)
			{
				for (int j = 0; j < _dsu.GetLength(1); ++j)
				{
					_dsu[i, j].GetParent();
				}
			}
		}

		//
		// </ DSU >
		//


		protected ERoCColor _GetColor(int row, int column)
		{
			var color = ERoCColor.NONE;

			var info = colorsManager.GetColorInfo(map[row, column]);
			if (info != null)
				color = info.color;

			return color;
		}

		protected bool[,] _was;
		protected Dictionary<int, RoCColorGroup> _groups;

		protected List<RoCColorGroup> _CollectGroups()
		{
			_groups = new Dictionary<int, RoCColorGroup>();

			for (int i = 0; i < mapRows; ++i)
			{
				for (int j = 0; j < mapColumns; ++j)
				{
					if (_dsu[i, j].color != ERoCColor.NONE)
					{
						var h = _dsu[i, j].pos.GetHashCode();
						if (!_groups.ContainsKey(h))
							_groups.Add(h, new RoCColorGroup(_dsu[i, j].color));
						_groups[h].AddPosition(_dsu[i, j].pos);
					}
				}
			}

			var list = new List<RoCColorGroup>();



			foreach (var kvp in _groups)
			{
				list.Add(kvp.Value);
			}

			return list;
		}

		public List<RoCColorGroup> Recognize(RGB[,] __map)
		{
			map = __map;

			_InitializeDSU(mapRows, mapColumns);
			_UniteAll();
			_RecalcDSUParents();

			return _CollectGroups();
		}

		[ContextMenu("Build")]
		public void Build()
		{

		}


		//
		// < Debug >
		//

		public bool debug = false;

		public void Log(object mess)
		{
			if (debug)
				Debug.Log(mess);
		}

		//
		// </ Debug >
		//
	}

}
