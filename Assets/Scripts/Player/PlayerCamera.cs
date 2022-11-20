using QuantumWeavers.Input;
using UnityEngine;

namespace QuantumWeavers.Player {
	public class PlayerCamera : MonoBehaviour {
		[SerializeField] private Transform Player;
		[SerializeField] private float MouseSensitivity = 100f;

		private InputHandler _input;

		private float _xRotation;
		private float _yRotation;

		private void Start() {
			_input = FindObjectOfType<InputHandler>();

			Player = transform.parent;
		}

		private void Update() {
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
