using QuantumWeavers.Components.Core;
using UnityEngine;

namespace QuantumWeavers.Components.Player {
	public class PlayerCamera {
		// Position of the player
		private readonly Transform _player;
		// Position ot the camera
		private readonly Transform _camera;
		// Sensitivity of the camera movement
		private readonly float _mouseSensitivity;
		// Input variable
		private readonly InputHandler _input;
		// Rotation of the x axis
		private float _xRotation;
		// Rotation of the y axis
		private float _yRotation;

		#region Constructor

		public PlayerCamera(Transform playerTransform, Transform cameraTransform, InputHandler input, float mouseSensitivity) {
			_player = playerTransform;
			_camera = cameraTransform;
			_input = input;
			_mouseSensitivity = mouseSensitivity;
		}
		
		#endregion

		#region Update

		public void TickUpdate() {
			Look();
		}

		#endregion

		#region Methods

		private void Look() {
			float mouseX = _input.GetLook().x * _mouseSensitivity * Time.deltaTime;
			float mouseY = _input.GetLook().y * _mouseSensitivity * Time.deltaTime;

			_xRotation -= mouseY;
			_xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            
			_camera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
			_player.Rotate(Vector3.up * mouseX);
		}
		
		#endregion

	}
}
