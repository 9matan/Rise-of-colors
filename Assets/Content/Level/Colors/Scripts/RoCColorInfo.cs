using UnityEngine;
using System.Collections;
using RoC;
using UniRx;

namespace RoC
{


	public class RoCColorInfo : ScriptableObject
	{


		[SerializeField]
		protected ERoCColor _color;
		[SerializeField]
		protected RGB _centerColor;
		[SerializeField]
		protected int _delta;

		public bool IsValidColor(RGB color)
		{
			return true;
		}

		public bool IsValidColor(Color32 color)
		{
			return true;
		}

	}

}
