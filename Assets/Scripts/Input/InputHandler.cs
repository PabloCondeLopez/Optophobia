using UnityEngine;
using UnityEngine.InputSystem;

namespace QuantumWeavers.Input {
    public class InputHandler : MonoBehaviour {

        private InputActions _input;
        private Vector2 _movement;
        private void Awake() {
            if (_input == null) {
                _input = new InputActions();
            }

            _input.PlayerInput.Look.performed += OnCameraMovement;
        }

        private void OnEnable() {
            _input.Enable();
        }

        private void OnDisable() {
            _input.Disable();
        }

        #region Getters

        public Vector2 GetMovement() {
            return _movement;
        }

        #endregion

        private void OnCameraMovement(InputAction.CallbackContext value) {
            if (value.performed)
                _movement = value.ReadValue<Vector2>();
            
            Debug.Log(_movement);
        }
    }
}
