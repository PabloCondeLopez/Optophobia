using OpenCvSharp.Demo;
using UnityEngine;
using UnityEngine.UI;

namespace QuantumWeavers.Components.FaceTracking {
	public class FaceDetectorV1 : MonoBehaviour {
		
		[SerializeField] private GameObject Surface;

		[SerializeField] private TextAsset Faces;
		[SerializeField] private TextAsset Eyes;

		private WebCamDevice? _webCamDevice;
		private WebCamTexture _webCamTexture;
		private Texture2D _renderedTexture;
		private bool _forceFrontalCamera;
		private OpenCvSharp.Unity.TextureConversionParams _textureParameters;
		private FaceProcessorLive<WebCamTexture> _processor;

		private string DeviceName {
			get => _webCamDevice?.name;

			set {
				if (value == DeviceName) return;
				
				if (_webCamTexture && _webCamTexture.isPlaying) _webCamTexture.Stop();

				int cameraIndex = -1;
				
				for(int i = 0; i < WebCamTexture.devices.Length; i++)
					if (WebCamTexture.devices[i].name == value)
						cameraIndex = i;

				if (cameraIndex == -1) return;
				
				_webCamDevice = WebCamTexture.devices[cameraIndex];
				_webCamTexture = new WebCamTexture(_webCamDevice.Value.name);

				UpdateTextureConversionParameters();
				
				_webCamTexture.Play();
			}
		}

		private void Awake() {
			if (WebCamTexture.devices.Length > 0) DeviceName = WebCamTexture.devices[0].name;
			_forceFrontalCamera = true;

			_processor = new FaceProcessorLive<WebCamTexture>();
			_processor.Initialize(Faces.text, Eyes.text);

			_processor.DataStabilizer.Enabled = true;
			_processor.DataStabilizer.Threshold = 2.0;
			_processor.DataStabilizer.SamplesCount = 2;

			_processor.Performance.Downscale = 256;
			_processor.Performance.SkipRate = 0;

		}

		private void Update() {
			if (_webCamTexture && _webCamTexture.didUpdateThisFrame) {
				UpdateTextureConversionParameters();
				
				if(ProcessTexture(_webCamTexture, ref _renderedTexture))
					RenderFrame();
			}
		}

		private void OnDestroy() {
			if (_webCamTexture != null) {
				if(_webCamTexture.isPlaying) _webCamTexture.Stop();
				_webCamTexture = null;
			}

			_webCamDevice = null;
		}

		private void UpdateTextureConversionParameters() {
			OpenCvSharp.Unity.TextureConversionParams parameters = new() {
				FlipHorizontally = _forceFrontalCamera || _webCamDevice.Value.isFrontFacing
			};

			if (_webCamTexture.videoRotationAngle != 0)
				parameters.RotationAngle = _webCamTexture.videoRotationAngle;

			_textureParameters = parameters;
		}

		private void RenderFrame() {
			if (!_renderedTexture) return;

			Surface.GetComponent<RawImage>().texture = _renderedTexture;
			Surface.GetComponent<RectTransform>().sizeDelta =
				new Vector2(_renderedTexture.width, _renderedTexture.height);
		}

		private bool ProcessTexture(WebCamTexture input, ref Texture2D output) {
			_processor.ProcessTexture(input, _textureParameters);
			
			_processor.MarkDetected();

			output = OpenCvSharp.Unity.MatToTexture(_processor.Image, output);

			return true;
		}
		
	}
}