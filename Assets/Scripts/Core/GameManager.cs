using UnityEngine;

namespace QuantumWeavers.Core
{
    public class GameManager : MonoBehaviour
    {

        #region Singleton

        public static GameManager Instance;

        private void Awake()
        {
            if (Instance != null) return;

            Instance = this;
        }

        #endregion

        [SerializeField] private InputHandler Input;

        public InputHandler GetInput()
        {
            return Input;
        }
    }
}
