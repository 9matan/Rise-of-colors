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

		public bool registeredFormat
		{
			get; protected set;
		}

		protected Image.PIXEL_FORMAT _pixelFormat;

		public void Initialize()
		{
			vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);
		}

		//
		// < Vuforia >
		//

		public void OnVuforiaStarted()
		{
			_RegisterFormat();
		}

		protected void _UnregisterFormat()
		{
			Debug.Log("Unregistering camera pixel format " + _pixelFormat.ToString());
			camera.SetFrameFormat(_pixelFormat, false);
			registeredFormat = false;
		}

		protected bool _RegisterFormat()
		{
			if (CameraDevice.Instance.SetFrameFormat(_pixelFormat, true))
			{
				Debug.Log("Successfully registered camera pixel format " + _pixelFormat.ToString());
				registeredFormat = true;
			}
			else
			{
				Debug.LogError("Failed to register camera pixel format " + _pixelFormat.ToString());
				registeredFormat = false;
			}

			return registeredFormat;
		}

		protected void OnPouse(bool paused)
		{
			if (paused)
			{
				Log("App was paused");
				_UnregisterFormat();
			}
			else
			{
				Log("App was resumed");
				_RegisterFormat();
			}
		}

		//
		// < Vuforia >
		//

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
