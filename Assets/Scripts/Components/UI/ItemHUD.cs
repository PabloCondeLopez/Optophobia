using UnityEngine;
using TMPro;

namespace QuantumWeavers.Components.UI {
	public class ItemHUD : MonoBehaviour {
		[SerializeField] private Camera PlayerCamera;

		private void Update() {
			Vector3 cameraPosition = PlayerCamera.transform.position;
			Vector3 direction = cameraPosition - transform.position;

			direction.x = direction.z = 0.0f;
			
			transform.LookAt(cameraPosition - direction);
			transform.Rotate(0, 180, 0);
		}

		public void Enable() {
			gameObject.SetActive(true);
		}

		public void Disable() {
			gameObject.SetActive(false);
		}
	}
}
