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
		[Tooltip("Movement type of the HUD")]
		[SerializeField] private Ease MovementType;
		[Tooltip("Animation speed of the UI")]
		[SerializeField] private float Speed = 2f;
		[Tooltip("Distance which the UI will jump")] 
		[SerializeField] private float JumpHeight;
		
		// Initial position of the UI
		private Vector3 _initialPosition;

		#region Unity Events

		private void OnEnable() {
			_initialPosition = transform.localPosition;
			transform.DOLocalMove(new Vector3(0, JumpHeight, 0), Speed).SetEase(MovementType).SetLoops(-1, LoopType.Yoyo);
		}

		private void OnDisable() {
			transform.DOPause();
			transform.localPosition = _initialPosition;
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
