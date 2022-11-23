using UnityEngine;
using QuantumWeavers.Components.Core;

namespace QuantumWeavers.Classes.Player {
    public class PlayerLocomotion {
        // Position of the player
        private readonly Transform _playerPosition;
        // Rigidbody component
        private readonly Rigidbody _rb;
        // Input handler component
        private readonly InputHandler _input;
        // Movement speed of the player
        private readonly float _playerSpeed;
        // Movement amount in the x axis of the player
        private float _xMovement;
        // Movement amount in the y axis of the player
        private float _zMovement;

        #region Constructor

        public PlayerLocomotion(Rigidbody rigidbody, InputHandler input, float speed, Transform playerPosition) {
            _rb = rigidbody;
            _playerSpeed = speed;
            _playerPosition = playerPosition;
            _input = input;
        }
        
        #endregion

        #region Update

        public void TickUpdate() {
            HandleMovement();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the player's movement
        /// </summary>
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

        #endregion

    }
}
