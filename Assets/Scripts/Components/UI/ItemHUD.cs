using System.Text;
using DG.Tweening;
using UnityEngine;
using TMPro;

namespace QuantumWeavers.Components.UI {
	public class ItemHUD : MonoBehaviour {
		[Tooltip("Camera attached to the character")]
		[SerializeField] private Camera PlayerCamera;
		[Tooltip("Text of the UI")]
		[SerializeField] private TextMeshProUGUI HUDText;
		[Tooltip("Animation speed of the UI")]
		[SerializeField] private float Speed = 2f;
		
		// Initial position of the UI
		private Vector3 _initialPosition;

		#region Unity Events

		private void OnEnable() {
			_initialPosition = transform.position;
			transform.DOLocalMove(new Vector3(0, 2f, 0), Speed).SetEase(Ease.OutCubic).SetLoops(-1, LoopType.Yoyo);
		}

		private void OnDisable() {
			transform.DOPause();
			transform.position = _initialPosition;
		}

		private void Update() {
			Vector3 cameraPosition = PlayerCamera.transform.position;
			Vector3 direction = cameraPosition - transform.position;

			direction.x = direction.z = 0.0f;
			
			transform.LookAt(cameraPosition - direction);
			transform.Rotate(0, 180, 0);
		}
		
		#endregion

		#region Methods

		/// <summary>
		/// Enables the item UI
		/// </summary>
		/// <param name="itemName">Name of the item which this UI is attached to</param>
		public void Enable() {
			gameObject.SetActive(true);
		}

		/// <summary>
		/// Disables the UI
		/// </summary>
		public void Disable() {
			gameObject.SetActive(false);
		}
		
		#endregion
		
	}
}
