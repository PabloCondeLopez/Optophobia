using QuantumWeavers.Components.Core;
using UnityEngine;

namespace QuantumWeavers.Components.Player {
	public class PlayerCamera {

        #region _privateVariables

        [Tooltip("Position of the player.")]
		private readonly Transform _player;
		[Tooltip("Position of the camera.")]
		private readonly Transform _camera;
		[Tooltip("Sensibility of the camera movement.")]
		private readonly float _mouseSensitivity;
		[Tooltip("Input variable.")]
		private readonly InputHandler _input;
		[Tooltip("Rotation of the x axis")]
		private float _xRotation;
		[Tooltip("Rotation of the y axis")]
		private float _yRotation;

		private bool _isFrozen = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Player camera constructor.
        /// </summary>
        /// <param name="playerTransform">Position of the player.</param>
        /// <param name="cameraTransform">Position of the camara.</param>
        /// <param name="input">Input variable.</param>
        /// <param name="mouseSensitivity">Sensitivity of the mouse.</param>
        public PlayerCamera(Transform playerTransform, Transform cameraTransform, InputHandler input, float mouseSensitivity) {
			_player = playerTransform;
			_camera = cameraTransform;
			_input = input;
			_mouseSensitivity = mouseSensitivity;
		}

		#endregion

		public void SetIsFrozen(bool aux)
		{
			_isFrozen = aux;
		}

		#region Update
		/// <summary>
		/// Calls for Look().
		/// </summary>
		public void TickUpdate() {
			if(!_isFrozen)Look();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Handles the camera rotation.
		/// </summary>
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
