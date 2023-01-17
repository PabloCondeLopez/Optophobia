using UnityEngine;
using UnityEngine.SceneManagement;
using QuantumWeavers.Components.Sound;
using QuantumWeavers.Components.Core;

namespace QuantumWeavers.Components.Menus {
    public class Menu : MonoBehaviour
    {
        #region Methods

        private void Start()
        {
            SoundManager.Instance.Play("MainMenuMusic");
        }
        /// <summary>
        /// Function that runs when the start button is clicked.
        /// </summary>
        public void OnStartButton()
        {
            GameManager.Instance.EyesOpen = false;
            GameManager.Instance.SetGameStates(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            PlayButton();
            SoundManager.Instance.Stop("MainMenuMusic");
        }

        public void OnDebugButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            PlayButton();
        }

        /// <summary>
        /// Function that runs when the exit button is clicked.
        /// </summary>
        public void OnExitButton()
        {
            Application.Quit();
            PlayButton();
        }

        /// <summary>
        /// Function that plays the general sound of a button.
        /// </summary>
        public void PlayButton()
        {
            SoundManager.Instance.Play("Button");
        }
        
        #endregion
    }
}
