using UnityEngine;
using UnityEngine.UI;
using QuantumWeavers.Components.Core;

namespace QuantumWeavers.Components.UI {
	public class HUDManager : MonoBehaviour {

        #region _privateVariables

        [Header("Images")]
		[Tooltip("Image of the eyes state")]
		[SerializeField] private Image EyesIndicator;
		[Tooltip("Screen shown when the player closes his eyes")]
		[SerializeField] private Image EyesClosed;
		[Header("Sprites")]
		[Tooltip("Sprite shown when the eyes are open")]
		[SerializeField] private Sprite EyesOpenSprite;
		[Tooltip("Sprite shown when the eyes are closed")]
		[SerializeField] private Sprite EyesClosedSprite;
		[Header("UI Elements")]
		[Tooltip("UI of the pause menu")]
		[SerializeField] private GameObject PauseMenu;

		[Header("Debug")]
		[Tooltip("True if the screen goes black when the eyes are closed. False otherwise")]
		[SerializeField] private bool SimulateClosedEyes;

		[Tooltip("GameManager instance.")]
		private GameManager _gameManager;

        #endregion

        #region Setters

		public void setSimulateClosedEyes(bool active)
        {
			SimulateClosedEyes = active;
        }

        #endregion

        #region Unity Methods

        private void Start() {
			setSimulateClosedEyes(true);
			_gameManager = GameManager.Instance;
		}

		private void Update() {
			PauseMenu.SetActive(_gameManager.GamePaused);
			HandleEyesSprite();
		}
		
		#endregion

		#region Methods

		/// <summary>
		/// Handles the behaviour of the eyes indicator.
		/// </summary>
		private void HandleEyesSprite() {
			EyesIndicator.sprite = _gameManager.EyesOpen ? EyesOpenSprite : EyesClosedSprite;
			
			if(SimulateClosedEyes)
				EyesClosed.enabled = !_gameManager.EyesOpen;
			else {
				EyesClosed.enabled = false;
			}
		}

		/// <summary>
		/// Handles the pause menu close button.
		/// </summary>
		public void OnCloseButton()
		{
			_gameManager.ResumeGame();
		}
		
		#endregion
		
	}
}
