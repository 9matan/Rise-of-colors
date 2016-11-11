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

	//
	// </ Color info >
	//

	public class RoCColorRecognationController : MonoBehaviour,
		IVOSBuilder
	{

		protected void Awake()
		{
			Initialize();
		}


		public void Initialize()
		{
			_ListenVuforia(vuforia);
		}

		protected void _ListenVuforia(VuforiaBehaviour __vuforia)
		{
			__vuforia.RegisterOnPauseCallback(OnPause);
			__vuforia.RegisterVuforiaStartedCallback(_OnVuforiaStarted);
			__vuforia.RegisterTrackablesUpdatedCallback(_OnTrackablesUpdated);
		}

		/*	protected void Update()
			{
				var img = camera.GetCameraImage(_pixelFormat);
				Debug.Log("Image: " + img.Width + " " + img.Height);
			}
			*/
		//
		// < Vuforia >
		//

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

		[SerializeField]
		protected Image.PIXEL_FORMAT _pixelFormat = Image.PIXEL_FORMAT.RGB888;

		protected void _OnVuforiaStarted()
		{
			_RegisterFormat();
		}

		protected void _OnTrackablesUpdated()
		{
			Log("OnFrame");

			if (registeredFormat)
			{
				Vuforia.Image image = CameraDevice.Instance.GetCameraImage(_pixelFormat);
				if (image != null)
				{
					string imageInfo = _pixelFormat + " image: \n";
					imageInfo += " size: " + image.Width + " x " + image.Height + "\n";
					imageInfo += " bufferSize: " + image.BufferWidth + " x " + image.BufferHeight + "\n";
					imageInfo += " stride: " + image.Stride;
					Log(imageInfo);
					/*	byte[] pixels = image.Pixels;
						if (pixels != null && pixels.Length > 0)
						{
							Debug.Log("Image pixels: " + pixels[0] + "," + pixels[1] + "," + pixels[2] + ",...");
						}*/
				}
			}
		}

		protected void _UnregisterFormat()
		{
			Log("Unregistering camera pixel format " + _pixelFormat.ToString());
			camera.SetFrameFormat(_pixelFormat, false);
			registeredFormat = false;
		}

		protected bool _RegisterFormat()
		{
			if (CameraDevice.Instance.SetFrameFormat(_pixelFormat, true))
			{
				Log("Successfully registered camera pixel format " + _pixelFormat.ToString());
				registeredFormat = true;
			}
			else
			{
				Debug.LogError("Failed to register camera pixel format " + _pixelFormat.ToString());
				registeredFormat = false;
			}

			return registeredFormat;
		}

		protected void OnPause(bool paused)
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
