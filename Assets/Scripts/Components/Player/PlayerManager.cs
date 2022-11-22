using QuantumWeavers.Components.Core;
using UnityEngine;

namespace QuantumWeavers.Components.Player {
    public class PlayerManager : MonoBehaviour {
        private PlayerLocomotion _locomotion;
        private PlayerCamera _camera;
        public GameManager GameManager { get; private set; }

        private void OnEnable() {
            _locomotion = GetComponent<PlayerLocomotion>();
            _camera = GetComponentInChildren<PlayerCamera>();
            GameManager = GameManager.Instance;
        }

        private void Update() {
            _camera.TickUpdate();
            _locomotion.TickUpdate();
        }
    }
}
