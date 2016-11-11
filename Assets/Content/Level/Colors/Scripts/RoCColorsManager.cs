using UnityEngine;
using System.Collections;
using RoC;
using UniRx;

namespace RoC
{



	public class RoCColorsManager : MonoBehaviour,
		IVOSBuilder
	{
		[SerializeField]
		protected RoCColorsInfoContainer _infoes;


		public RoCColorInfo GetColorInfo(ERoCColor color)
		{
			return _infoes[color];
		}

		public RoCColorInfo GetColorInfo(RGB color)
		{
			RoCColorInfo info = null;

			foreach (var kvp in _infoes)
			{
				if (kvp.Value.IsValidColor(color))
				{
					info = kvp.Value;
					break;
				}
			}

			return info;
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

	[System.Serializable]
	public class RoCColorsInfoContainer : VOSSerializableDictionary<ERoCColor, RoCColorInfo>
	{

	}

}
