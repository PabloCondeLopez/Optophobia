using UnityEngine;
using QuantumWeavers.Components.Core;

namespace QuantumWeavers.Classes.Player {
    [System.Serializable]
    public class PlayerLocomotion {

        private Transform _playerPosition;
        
        private readonly Rigidbody _rb;
        private readonly InputHandler _input;

        private float _playerSpeed;
        private float _xMovement;
        private float _zMovement;

        public PlayerLocomotion(Rigidbody rigidbody, InputHandler input, float speed, Transform playerPosition) {
            _rb = rigidbody;
            _playerSpeed = speed;
            _playerPosition = playerPosition;
            _input = input;
        }

        public void TickUpdate() {
            HandleMovement();
        }

        private void HandleMovement() {
            _xMovement = _input.GetMovement().x;
            _zMovement = _input.GetMovement().y;
            
            if(!GameManager.Instance.EyesOpen) {
                Vector3 forward = _playerPosition.transform.forward.normalized;
                Vector3 right = _playerPosition.transform.right.normalized;

                forward.y = 0;
                right.y = 0;

                Vector3 forwardRelative = _zMovement * forward;
                Vector3 rightRelative = _xMovement * right;

                Vector3 cameraRelativeMovement = forwardRelative + rightRelative;
                
                _rb.MovePosition(_rb.position + _playerSpeed * Time.deltaTime * cameraRelativeMovement);
            }
        }
    }
}
