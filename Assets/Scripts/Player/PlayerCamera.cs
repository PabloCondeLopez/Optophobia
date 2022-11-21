using QuantumWeavers.Core;
using QuantumWeavers.Input;
using UnityEngine;

namespace QuantumWeavers.Player {
	public class PlayerCamera : MonoBehaviour {
		[SerializeField] private Transform Player;
		[SerializeField] private Transform ItemDetectionPosition;
		[SerializeField] private LayerMask ItemMask;
		[SerializeField] private float ItemDetectionRadius = 0.5f;
		[SerializeField] private float MouseSensitivity = 100f;

		private InputHandler _input;

		private float _xRotation;
		private float _yRotation;

		private Collider _previousCollider;
		private static readonly int Outline = Shader.PropertyToID("_Outline");

		private void Start() {
			_input = GameManager.Instance.GetInput();

			Player = transform.parent;
		}

		private void Update() {
			Look();
			SeekItems();
		}

		private void Look() {
			float mouseX = _input.GetLook().x * MouseSensitivity * Time.deltaTime;
			float mouseY = _input.GetLook().y * MouseSensitivity * Time.deltaTime;

			_xRotation -= mouseY;
			_xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            
			transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
			Player.Rotate(Vector3.up * mouseX);
		}

		private void SeekItems() {
			Collider[] colliders = Physics.OverlapSphere(transform.position + Vector3.up,
				2.0f, ItemMask);
			
			foreach (Collider col in colliders) {
				if(_previousCollider != col) {
					if (col != null) {
						col.GetComponent<Renderer>().material.SetFloat(Outline, 0.01f);
						_previousCollider = col;
						return;
					}
				}
			}
			
			if(colliders.Length == 0 && _previousCollider) {
				_previousCollider.GetComponent<Renderer>().material.SetFloat(Outline, 0f);
				_previousCollider = null;
			}
			
		}

		private void OnDrawGizmos() {
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(ItemDetectionPosition.position, ItemDetectionRadius);
		}
	}
}
