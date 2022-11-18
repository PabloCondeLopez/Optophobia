using UnityEngine;
using UnityEngine.InputSystem;

namespace QuantumWeavers.Input {
    public class InputHandler : MonoBehaviour {
        private PlayerInput _input;
        private Vector2 _movement;
        private void Awake() {
            if (_input == null) {
                _input = new PlayerInput();
            }

            _input._locomotion._movement.performed += OnJoystickMovement;
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

        private void OnJoystickMovement(InputAction.CallbackContext value) {
            if (value.performed)
                _movement = value.ReadValue<Vector2>();
            
            Debug.Log(_movement);
        }
    }
}
