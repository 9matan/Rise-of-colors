using UnityEngine;
using System.Collections;
using RoC;
using UniRx;

namespace RoC
{


	public struct RGB
	{
		public byte r;
		public byte g;
		public byte b;

		public RGB(byte __r, byte __g, byte __b)
		{
			r = __r;
			g = __g;
			b = __b;
		}

		public RGB(Color32 color)
		{
			r = color.r;
			g = color.g;
			b = color.b;
		}

	}

	public class RoCColorRecognation : MonoBehaviour,
		IVOSBuilder
	{

	
		[ContextMenu("Build")]
		public void Build()
		{
		
		}		
	}
	
}
