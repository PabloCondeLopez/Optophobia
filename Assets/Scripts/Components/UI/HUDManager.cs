using UnityEngine;
using UnityEngine.UI;
using QuantumWeavers.Components.Core;

namespace QuantumWeavers.Components.UI {
	public class HUDManager : MonoBehaviour {
		[SerializeField] private Image EyesIndicator;
		[SerializeField] private Image EyesClosed;
		[SerializeField] private Sprite EyesOpenSprite;
		[SerializeField] private Sprite EyesClosedSprite;

		private GameManager _gameManager;

		private void Start() {
			_gameManager = GameManager.Instance;
		}

		private void Update() {
			HandleEyesSprite();
		}

		private void HandleEyesSprite() {
			EyesIndicator.sprite = _gameManager.EyesOpen ? EyesOpenSprite : EyesClosedSprite;
			//EyesClosed.enabled = !_gameManager.EyesOpen;
		}
	}
}
