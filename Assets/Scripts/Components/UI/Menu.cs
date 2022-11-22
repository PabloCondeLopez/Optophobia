using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QuantumWeavers.Components.Sound;

namespace QuantumWeavers.Components.UI
{
    public class Menu : MonoBehaviour
    {
        /// <summary>
        /// Function that runs when the start button is clicked.
        /// </summary>
        public void OnStartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    }
}
