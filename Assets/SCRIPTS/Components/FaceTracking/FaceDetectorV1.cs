using UnityEngine;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.ObjdetectModule;
using Rect = OpenCVForUnity.CoreModule.Rect;

namespace QuantumWeavers.Components.FaceTracking {
	public class FaceDetectorV1 : MonoBehaviour {
		private bool _firstClose = false;

		[SerializeField] private bool DebugMode = false;
		[SerializeField] private MeshRenderer Material;

		private CascadeClassifier _faceCascade;
		private CascadeClassifier _eyeCascade;
		private WebCamTexture _webCamTexture;
		private Mat _rgbaMat;
		private Mat _grayMat;

		private void Start() {
			string facePath = Application.dataPath + "/SCRIPTS/Components/FaceTracking/haarcascade_frontalface_default.xml";
			string eyePath = Application.dataPath + "/SCRIPTS/Components/FaceTracking/haarcascade_eye.xml";

			_faceCascade = new CascadeClassifier(facePath);
			_eyeCascade = new CascadeClassifier(eyePath);

			_webCamTexture = new WebCamTexture();
			_webCamTexture.Play();
			Material.material.mainTexture = _webCamTexture;

			_rgbaMat = new Mat(_webCamTexture.height, _webCamTexture.width, CvType.CV_8UC4);
			_grayMat = new Mat(_webCamTexture.height, _webCamTexture.width, CvType.CV_8UC1);
		}

		private void Update() {
			Imgproc.cvtColor(_rgbaMat, _grayMat, Imgproc.COLOR_RGBA2GRAY);
			Imgproc.bilateralFilter(_grayMat, _grayMat, 5, 1, 1);

			MatOfRect faces = new MatOfRect();
			_faceCascade.detectMultiScale(_grayMat, faces, 1.3, 5, 0, new Size(50, 50));

			if (faces.toArray().Length <= 0) {
				Imgproc.putText(_rgbaMat, "No face detected", new Point(100, 100),
					Imgproc.FONT_HERSHEY_PLAIN, 3, new Scalar(0, 255, 0), 2);
				Debug.Log("No face detected");
				return;
			}

			foreach (Rect face in faces.toArray()) {
				Mat roiFace = _grayMat.submat(face);
				Mat roiFaceColor = _rgbaMat.submat(face);
				
				MatOfRect eyes = new MatOfRect();
				_eyeCascade.detectMultiScale(roiFace, eyes, 1.3, 5, 0, new Size(10, 10));

				if (eyes.toArray().Length >= 2) {
					if(DebugMode)
						Imgproc.putText(_rgbaMat, "Eyes Open!", new Point(face.x, face.y - 20), 
							Imgproc.FONT_HERSHEY_PLAIN, 2, new Scalar(255, 255, 255), 2);

					foreach (Rect eye in eyes.toArray()) 
						Imgproc.rectangle(roiFaceColor, eye.tl(), eye.br(), new Scalar(255, 0, 0), 2);

					_firstClose = false;
				}
				else {
					if(DebugMode)
						Imgproc.putText(_rgbaMat, "Eyes Closed", new Point(face.x, face.y - 20), 
							Imgproc.FONT_HERSHEY_PLAIN, 3, new Scalar(0, 0, 255), 2);

					if (_firstClose) {
						Debug.Log("Eyes closed, you can advance");
						_firstClose = true;
					}
				}
			}
		}
	}
}