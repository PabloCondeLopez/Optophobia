using UnityEngine;
using UnityEngine.InputSystem;

namespace QuantumWeavers.Input {
    public class InputHandler : MonoBehaviour {
        private InputActions _input;
        private Vector2 _look;
        
        private void Awake() {
            if (_input == null) {
                _input = new InputActions();
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update() {
            OnLook();
        }

        private void OnLook() {
            _look = _input.Player.Look.ReadValue<Vector2>();
        }

        public Vector2 GetLook() {
            return _look;
        }

        private void OnEnable() {
            _input.Enable();
        }

        private void OnDisable() {
            _input.Disable();
        }
    }
}
