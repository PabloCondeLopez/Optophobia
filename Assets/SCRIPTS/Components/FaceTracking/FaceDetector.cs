using System;
using UnityEngine;
using OpenCvSharp;

namespace QuantumWeavers.Components.FaceTracking {
	public class FaceDetector : MonoBehaviour {
		private WebCamTexture _webCamTexture;
		private CascadeClassifier _cascade;
		private OpenCvSharp.Rect _face;
		private Renderer _renderer;

		private void Start() {
			WebCamDevice[] devices = WebCamTexture.devices;
			_renderer = GetComponent<Renderer>();

			_webCamTexture = new WebCamTexture(devices[0].name);
			_webCamTexture.Play();

			_cascade = new CascadeClassifier(System.IO.Path.Combine(Application.dataPath, "haarcascade_frontalface_default.xml"));
		}

		private void Update() {
			_renderer.material.mainTexture = _webCamTexture;
			Mat frame = OpenCvSharp.Unity.TextureToMat(_webCamTexture);
			
			FindNewFace(frame);
			Display(frame);
		}

		private void FindNewFace(Mat frame) {
			OpenCvSharp.Rect[] faces = _cascade.DetectMultiScale(frame, 1.1, 2, HaarDetectionType.ScaleImage);

			if (faces.Length >= 1) {
				_face = faces[0];
			}
		}

		private void Display(Mat frame) {
			if (_face != null) {
				frame.Rectangle(_face, new Scalar(250, 0, 0), 2);
			}

			Texture newTexture = OpenCvSharp.Unity.MatToTexture(frame);
			_renderer.material.mainTexture = newTexture;
		}
	}
}