using UnityEngine;
using QuantumWeavers.Components.Core;
using QuantumWeavers.Components.Sound;

namespace QuantumWeavers.Classes.Player {
    public class PlayerLocomotion {

        #region _privateVariables

        [Tooltip("Position of the player.")]
        private readonly Transform _playerPosition;
        [Tooltip("Rigidbody component.")]
        private readonly Rigidbody _rb;
        [Tooltip("Input handler component.")]
        private readonly InputHandler _input;
        [Tooltip("Movement speed of the player.")]
        private readonly float _playerSpeed;
        [Tooltip("Movement amount in the x axis of the player")]
        private float _xMovement;
        [Tooltip("Movement amount in the y axis of the player")]
        private float _zMovement;

        private bool _isFrozen = false;

        [Tooltip("Time between footsteps")]
        private float _footStepTimer;

        #endregion

        #region Constructor
        public PlayerLocomotion(Rigidbody rigidbody, InputHandler input, float speed, Transform playerPosition) {
            _rb = rigidbody;
            _playerSpeed = speed;
            _playerPosition = playerPosition;
            _input = input;
        }
        
        #endregion

        public void SetIsFrozen(bool aux)
        {
            _isFrozen = aux;
        }

        #region Update

        /// <summary>
        /// Calls for HandleMovement().
        /// </summary>
        public void TickUpdate() {
            if(!_isFrozen) HandleMovement();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the player's movement
        /// </summary>
        private void HandleMovement() {
            _xMovement = _input.GetMovement().x;
            _zMovement = _input.GetMovement().y;

            if (GameManager.Instance.EyesOpen) {
                SoundManager.Instance.Stop("Footstep_1");
                return;
            }
            
            Vector3 forward = _playerPosition.transform.forward.normalized;
            Vector3 right = _playerPosition.transform.right.normalized;

            forward.y = 0;
            right.y = 0;

            Vector3 forwardRelative = _zMovement * forward;
            Vector3 rightRelative = _xMovement * right;

            Vector3 cameraRelativeMovement = forwardRelative + rightRelative;
                
            _rb.MovePosition(_rb.position + _playerSpeed * Time.deltaTime * cameraRelativeMovement);

            if (cameraRelativeMovement == Vector3.zero) return;
            
            HandleFootsteps();
        }

        private void HandleFootsteps() {
            _footStepTimer -= Time.deltaTime;

            if (_footStepTimer > 0) return;

            SoundManager.Instance.Play("Footstep_1");
            _footStepTimer = 0.5f;
        }

        #endregion

    }
}
