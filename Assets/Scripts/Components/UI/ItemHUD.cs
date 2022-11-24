using DG.Tweening;
using UnityEngine;
using TMPro;

namespace QuantumWeavers.Components.UI {
	public class ItemHUD : MonoBehaviour {

        #region _privateVariables

        [Tooltip("Camera attached to the character.")]
		[SerializeField] private Camera _playerCamera;
		[Tooltip("Text of the UI.")]
		[SerializeField] private TextMeshProUGUI _HUDText;
		[Tooltip("Movement type of the HUD.")]
		[SerializeField] private Ease _movementType;
		[Tooltip("Animation speed of the UI.")]
		[SerializeField] private float _speed = 2f;
		[Tooltip("Distance which the UI will jump.")] 
		[SerializeField] private float _jumpHeight;

		[Tooltip("Initial position of the UI.")]
		private Vector3 _initialPosition;

		#endregion

		#region Unity Events

		/// <summary>
		/// When the object is enabled it initializes it's position and animates it.
		/// </summary>
		private void OnEnable() {
			_initialPosition = transform.localPosition;
			transform.DOLocalMove(new Vector3(0, _jumpHeight, 0), _speed).SetEase(_movementType).SetLoops(-1, LoopType.Yoyo);
		}
		
		/// <summary>
		/// Pauses the animation and resets the position of the item.
		/// </summary>
		private void OnDisable() {
			transform.DOPause();
			transform.localPosition = _initialPosition;
		}

		/// <summary>
		/// Makes the UI face the player.
		/// </summary>
		private void Update() {
			Vector3 cameraPosition = _playerCamera.transform.position;
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
