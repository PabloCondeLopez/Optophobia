using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QuantumWeavers.Sound;

namespace QuantumWeavers.UI
{
    public class Menu : MonoBehaviour
    {
        /// <summary>
        /// Function that runs when the start button is clicked.
        /// </summary>
        public void OnStartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SoundManager.Instance.Play("Button");
        }

        /// <summary>
        /// Function that runs when the exit button is clicked.
        /// </summary>
        public void OnExitButton()
        {
            Application.Quit();
            SoundManager.Instance.Play("Button");
        }

        public void PlayButton()
        {
            SoundManager.Instance.Play("button");
        }

    }
}
