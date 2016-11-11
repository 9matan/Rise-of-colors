using UnityEngine;
using System.Collections;
using RoC;
using UniRx;

namespace RoC
{

	[CreateAssetMenu(menuName = "RiseOfColors/Color/Info")]
	public class RoCColorInfo : ScriptableObject
	{

		public ERoCColor color
		{
			get { return _color; }
		}

		public RGB centerColor
		{
			get { return _centerColor; }
		}

		[SerializeField]
		protected ERoCColor _color;
		[SerializeField]
		protected RGB _centerColor;
		[SerializeField]
		protected int _delta;

		public bool IsValidColor(RGB color)
		{
			return _centerColor.Delta(color) <= _delta;
		}

		public bool IsValidColor(Color32 color)
		{
			return _centerColor.Delta(new RGB(color)) <= _delta;
		}

	}

}
