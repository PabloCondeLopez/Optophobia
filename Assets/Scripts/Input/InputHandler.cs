using UnityEngine;
using UnityEngine.InputSystem;

namespace QuantumWeavers.Input {
    public class InputHandler : MonoBehaviour {
        public static InputHandler Instance;
        private PlayerInput _input;

        [HideInInspector] 
        public Vector2 Movement;
        private void Awake() {
            if (Instance == null) Instance = this;

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

        private void OnJoystickMovement(InputAction.CallbackContext value) {
            if (value.performed)
                Movement = value.ReadValue<Vector2>();
            
            Debug.Log(Movement);
        }
    }
}
