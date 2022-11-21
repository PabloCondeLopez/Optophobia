using UnityEngine;

namespace QuantumWeavers.Input {
    public class InputHandler : MonoBehaviour {

        private InputActions _input;
        private Vector2 _look;
        private Vector2 _movement;

        private bool _eyesOpenButton;
        
        private void Awake() {
            if (_input == null) {
                _input = new InputActions();
            }

            Cursor.lockState = CursorLockMode.Locked;

            _input.Player.DebugEyes.performed += i => _eyesOpenButton = !_eyesOpenButton;
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

        public bool EyesOpen() {
            return _eyesOpenButton;
        }

        private void OnEnable() {
            _input.Enable();
        }

        private void OnDisable() {
            _input.Disable();
        }
    }
}
