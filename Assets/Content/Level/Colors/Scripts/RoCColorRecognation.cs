using UnityEngine;
using System.Collections;
using RoC;
using UniRx;

namespace RoC
{

	public enum ERoCColor
	{
		RED,
		GREEN,
		BLUE
	}

	[System.Serializable]
	public class RoCColorGroup
	{

		public Vector2 centerPosition
		{
			get { return Vector2.zero; }
		}

		public int number
		{
			get { return _number; }
		}

		[SerializeField]
		protected ERoCColor _color;
		[SerializeField]
		protected int _number;

	}

	public class RoCColorRecognation : MonoBehaviour,
		IVOSBuilder
	{



		public void Recognize()
		{

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
