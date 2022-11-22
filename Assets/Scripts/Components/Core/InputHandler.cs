using UnityEngine;

namespace QuantumWeavers.Components.Core {
    public class InputHandler : MonoBehaviour {

        private InputActions _input;
        private Vector2 _look;
        private Vector2 _movement;

        private void Awake() {
            if (_input == null) {
                _input = new InputActions();
            }

            DontDestroyOnLoad(this);
        }

        private void Update() {
            OnLook();
            OnMove();
        }

        private void OnLook() {
            _look = _input.Player.Look.ReadValue<Vector2>();
        }

        public Vector2 GetLook() {
            return _look;
        }

        private void OnMove() {
            _movement = _input.Player.Movement.ReadValue<Vector2>();
        }

        public Vector2 GetMovement() {
            return _movement;
        }

        public bool OnInteract() {
            return _input.Player.Interact.WasPressedThisFrame();
        }

        public bool OnPause() {
            return _input.Player.Pause.WasPressedThisFrame();
        }

        public bool EyesHandler() {
            return _input.Player.DebugEyes.WasPressedThisFrame();
        }

        private void OnEnable() {
            _input.Enable();
        }

        private void OnDisable() {
            _input.Disable();
        }
    }
}
