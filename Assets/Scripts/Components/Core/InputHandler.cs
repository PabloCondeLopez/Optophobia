using UnityEngine;

namespace QuantumWeavers.Components.Core {
    public class InputHandler : MonoBehaviour {

        // Player input actions. Defined outside the script
        private InputActions _input;
        // Camera look vector
        private Vector2 _look;
        // Movement vector
        private Vector2 _movement;

        #region Initialization

        private void Awake() {
            if (_input == null) {
                _input = new InputActions();
            }

            DontDestroyOnLoad(this);
        }
        
        #endregion

        #region Unity Events

        private void Update() {
            OnLook();
            OnMove();
        }
        
        private void OnEnable() {
            _input.Enable();
        }

        private void OnDisable() {
            _input.Disable();
        }
        
        #endregion

        #region Getters
        
        /// <summary>
        /// Gets the player's camera look vector
        /// </summary>
        /// <returns>A vector2 representing the look of the camera</returns>
        public Vector2 GetLook() {
            return _look;
        }
        
        /// <summary>
        /// Gets the player's movement vector
        /// </summary>
        /// <returns>A vector2 representing the player's movement</returns>
        public Vector2 GetMovement() {
            return _movement;
        }

        #endregion

        #region Input Handlers

        /// <summary>
        /// Reads the camera's look vector2
        /// </summary>
        private void OnLook() {
            _look = _input.Player.Look.ReadValue<Vector2>();
        }
        
        /// <summary>
        /// Reads the movement vector2
        /// </summary>
        private void OnMove() {
            _movement = _input.Player.Movement.ReadValue<Vector2>();
        }

        /// <summary>
        /// Handles when the player press the interact key
        /// </summary>
        /// <returns>True if it was pressed this frame. False otherwise</returns>
        public bool OnInteract() {
            return _input.Player.Interact.WasPressedThisFrame();
        }

        /// <summary>
        /// Handles when the player press the pause key
        /// </summary>
        /// <returns>True if it was pressed this frame. False otherwise</returns>
        public bool OnPause() {
            return _input.Player.Pause.WasPressedThisFrame();
        }

        /// <summary>
        /// Handles when the player press the eyes debug button
        /// </summary>
        /// <returns>True if it was pressed this frame. False otherwise</returns>
        public bool EyesHandler() {
            return _input.Player.DebugEyes.WasPressedThisFrame();
        }
        
        #endregion

    }
}
