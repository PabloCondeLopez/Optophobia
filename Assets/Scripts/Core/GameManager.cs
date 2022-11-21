using System;
using QuantumWeavers.Input;
using UnityEngine;

namespace QuantumWeavers.Core
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
        }
        
        #endregion
        [Tooltip("InputHandler.")]
        [SerializeField] private InputHandler Input;

        public bool EyesOpen { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Input.</returns>
        public InputHandler GetInput()
        {
            return Input;
        }

        private void Update() {
            EyesOpen = Input.EyesOpen();
        }
    }
}
