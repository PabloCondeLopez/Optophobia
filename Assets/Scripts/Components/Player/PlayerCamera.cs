using QuantumWeavers.Components.Core;
using UnityEngine;

namespace QuantumWeavers.Components.Player {
	public class PlayerCamera : MonoBehaviour {
		[SerializeField] private Transform Player;
		[SerializeField] private float MouseSensitivity = 100f;

		private InputHandler _input;

		private float _xRotation;
		private float _yRotation;

		private void Start() {
			_input = GameManager.Instance.GetInput();

			Player = transform.parent;
		}

		public void TickUpdate() {
			Look();
		}

		private void Look() {
			float mouseX = _input.GetLook().x * MouseSensitivity * Time.deltaTime;
			float mouseY = _input.GetLook().y * MouseSensitivity * Time.deltaTime;

			_xRotation -= mouseY;
			_xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            
			transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
			Player.Rotate(Vector3.up * mouseX);
		}
	}
}
