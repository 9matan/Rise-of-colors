using UnityEngine;
using System.Collections;
using RoC;
using UniRx;

namespace RoC
{

	public class RoCColorsManager : MonoBehaviour,
		IVOSBuilder
	{




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
