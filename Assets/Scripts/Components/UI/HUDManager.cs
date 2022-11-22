using UnityEngine;
using UnityEngine.UI;
using QuantumWeavers.Components.Core;

namespace QuantumWeavers.Components.UI {
	public class HUDManager : MonoBehaviour {
		[SerializeField] private Image EyesIndicator;
		[SerializeField] private Image EyesClosed;
		[SerializeField] private Sprite EyesOpenSprite;
		[SerializeField] private Sprite EyesClosedSprite;
		[SerializeField] private GameObject PauseMenu;
		[SerializeField] private bool SimulateClosedEyes;

		private GameManager _gameManager;

		private void Start() {
			_gameManager = GameManager.Instance;
		}

		private void Update() {
			PauseMenu.SetActive(_gameManager.GamePaused);
			HandleEyesSprite();
		}

		private void HandleEyesSprite() {
			EyesIndicator.sprite = _gameManager.EyesOpen ? EyesOpenSprite : EyesClosedSprite;
			
			if(SimulateClosedEyes)
				EyesClosed.enabled = !_gameManager.EyesOpen;
			else {
				EyesClosed.enabled = false;
			}
		}

		public void OnCloseButton()
		{
			_gameManager.ResumeGame();
		}
	}
}
