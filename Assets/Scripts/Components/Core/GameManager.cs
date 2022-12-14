using UnityEngine;
using QuantumWeavers.Shared;

namespace QuantumWeavers.Components.Core {
    public class GameManager : MonoBehaviour {
        #region Singleton
        [Tooltip("Instance of GameManager, so it can be accessed from other classes.")]
        public static GameManager Instance;
        
        /// <summary>
        /// Initializes Instance.
        /// </summary>
        private void Awake()
        { 
            if (Instance != null) return;

            Instance = this;
            Input = GetComponent<InputHandler>();
            
            DontDestroyOnLoad(this);
        }

        #endregion

        #region Properties

        [Tooltip("Input handler component.")]
        public InputHandler Input { get; private set; }
        [Tooltip("Checks if the eyes are currently open.")]
        public bool EyesOpen { get; set; }
        [Tooltip("Checks if the game is paused.")]
        public bool GamePaused { get; set; }

        #endregion

        #region _privateVariables
        [Tooltip("Determines the state of the game.")]
        private GameStates _state;
        #endregion

        #region Getters&Setters

        public GameStates GetGameStates() { return _state; }
        public void SetGameStates(bool state) { _state = state ? GameStates.Playing : GameStates.Pause; }

        #endregion

        #region Unity Events

        private void Start()
        {
            EyesOpen = true;
            //_state = GameStates.Playing;
        }

        /// <summary>
        /// Handles the states of the game and the eyes handler.
        /// </summary>
        private void Update() {
            GamePaused = _state == GameStates.Pause;
            
            Cursor.lockState = GamePaused ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = GamePaused;
            
            if (Input.OnPause() && _state != GameStates.Pause) {
                _state = GameStates.Pause;
            } else if (Input.OnPause() && _state == GameStates.Pause) {
                _state = GameStates.Playing;
            }

            if (Input.EyesHandler())
                EyesOpen = !EyesOpen;
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Continues the game setting the game state to playing.
        /// </summary>
        public void ResumeGame() {
            _state = GameStates.Playing;
            GamePaused = false;
        }
        
        #endregion
    }
}
