using UnityEngine;
using QuantumWeavers.Shared;

namespace QuantumWeavers.Components.Core
{
    public class GameManager : MonoBehaviour
    {
        
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
        
        [Tooltip("InputHandler.")]
        private InputHandler Input;

        public bool EyesOpen { get; private set; }
        public bool GamePaused { get; private set; }

        private GameStates _state;
        
        
        /// <summary>
        /// Gets the input of the game
        /// </summary>
        /// <returns>Input of the game</returns>
        public InputHandler GetInput() {
            return Input;
        }

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

        public void ResumeGame() {
            _state = GameStates.Playing;
            GamePaused = false;
        }
    }
}
