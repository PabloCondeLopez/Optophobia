using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using QuantumWeavers.Components.Player;

namespace QuantumWeavers.Components.UI
{
   
    public class Pause : MonoBehaviour
    {
        [Tooltip("Default settings.")]
        public Button DefaultSettingsButton;
        [Tooltip("Slider that controls the mouse sensitivity.")]
        public Slider MouseSensitivitySlider;
        [Tooltip("Text that shows the sensibility.")]
        public TextMeshProUGUI MouseSensitivityText;
        public PlayerManager Player;
        [Tooltip("Range of sensibility the player can select from.")]
        private Vector2 _rangeOfSensitivity = new Vector2(1, 50);

        /// <summary>
        /// It initializes the MouseSensitivity sliders and text according to the initial MouseSesintivity of the Player.
        /// </summary>
        private void Start()
        {
            MouseSensitivitySlider.value = Player.GetMouseSensitivity() * _rangeOfSensitivity.x / _rangeOfSensitivity.y;
            MouseSensitivityText.text = Player.GetMouseSensitivity().ToString();
        }

        /// <summary>
        /// Function called when the menu is opened.
        /// </summary>
        public void OnOpen()
        {
            //TODO - if (DefaultSettingsButton != null) DefaultSettingsButton.onClick.Invoke();
        }

        /// <summary>
        /// Its sets mouse sensibility to a value equivalent to the value of the slider transformed to de _rangeOfSensitivity. 
        /// </summary>
        public void OnSlider()
        {
            Player.SetMouseSensitivity(Mathf.FloorToInt(MouseSensitivitySlider.value * _rangeOfSensitivity.y / _rangeOfSensitivity.x));
            MouseSensitivityText.text = Player.GetMouseSensitivity().ToString();
        }

    }

    
}

