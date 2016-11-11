using UnityEngine;
using System.Collections;
using RoC;
using UniRx;
using Vuforia;

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

		public new CameraDevice camera
		{
			get { return CameraDevice.Instance; }
		}

		public VuforiaBehaviour vuforia
		{
			get { return VuforiaBehaviour.Instance; }
		}


		public void Initialize()
		{
			vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);
		}

		public void OnVuforiaStarted()
		{
			if (camera.SetFrameFormat(Image.PIXEL_FORMAT.RGB888, true))
			{

			}
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
