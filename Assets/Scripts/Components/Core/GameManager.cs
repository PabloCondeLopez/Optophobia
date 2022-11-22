using System;
using UnityEngine;

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
        
        /// <summary>
        /// Gets the input of the game
        /// </summary>
        /// <returns>Input of the game</returns>
        public InputHandler GetInput() {
            return Input;
        }

        private void Update() {
            EyesOpen = Input.EyesOpen();
        }
    }
}
