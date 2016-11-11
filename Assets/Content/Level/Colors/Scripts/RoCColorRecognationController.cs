using UnityEngine;
using System.Collections;
using RoC;
using UniRx;
using Vuforia;

namespace RoC
{

	//
	// < Color info >
	//

	[System.Serializable]
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

		public int Delta(RGB color)
		{
			return
				Mathf.Abs(color.r - r)
				+ Mathf.Abs(color.b - b)
				+ Mathf.Abs(color.g - g);
		}

	}

	//
	// </ Color info >
	//

	[RequireComponent(typeof(RoCColorRecognation))]
	public class RoCColorRecognationController : MonoBehaviour,
		IVOSBuilder
	{

		protected RoCColorRecognation _recognation;

		protected void Awake()
		{
			_recognation = GetComponent<RoCColorRecognation>();
		}


		public RGB[,] ConvertToRGB(Image img)
		{
			RGB[,] map = new RGB[img.Width, img.Height];

			for (int i = 0; i < img.Width; ++i)
			{
				for (int j = 0; j < img.Height; ++j)
				{
					map[i, j] = new RGB(
						img.Pixels[i * img.Stride + j],
						img.Pixels[i * img.Stride + j + 1],
						img.Pixels[i * img.Stride + j + 2]);
				}
			}

			return map;
		}

		public void UpdateImage(RGB[,] map)
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
