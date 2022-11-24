using TMPro;
using UnityEngine;
using UnityEngine.UI;
using QuantumWeavers.Components.Player;

namespace QuantumWeavers.Components.Menus {
    public class Pause : MonoBehaviour
    {
        #region Variables

        [Tooltip("Default settings.")]
        [SerializeField] private Button DefaultSettingsButton;
        [Tooltip("Slider that controls the mouse sensitivity.")]
        [SerializeField] private Slider MouseSensitivitySlider;
        [Tooltip("Text that shows the sensibility.")]
        [SerializeField] private TextMeshProUGUI MouseSensitivityText;
        [SerializeField] private PlayerManager Player;
        [Tooltip("Range of sensibility the player can select from.")]
        private readonly Vector2 _rangeOfSensitivity = new Vector2(1, 50);

        #endregion

        #region Unity Events

        /// <summary>
        /// It initializes the MouseSensitivity sliders and text according to the initial MouseSesintivity of the Player.
        /// </summary>
        private void Start()
        {
            MouseSensitivitySlider.value = Player.GetMouseSensitivity() * _rangeOfSensitivity.x / _rangeOfSensitivity.y;
            MouseSensitivityText.text = Player.GetMouseSensitivity().ToString();
        }
        
        #endregion

        #region Methods

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
        
        #endregion
        

    }
}

