using UnityEngine;
using System.Collections;
using RoC;
using UniRx;

namespace RoC
{

	public static class RoCDirecrot
	{


		public static RoCColorsManager colorsManager
		{
			get
			{
				if (_colorsManager == null)
					_colorsManager = GameObject.FindObjectOfType<RoCColorsManager>();

				return _colorsManager;
			}
		}

		private static RoCColorsManager _colorsManager = null;

	}

}
