using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using QuantumWeavers.Components.Player;
using QuantumWeavers.Components.Sound;
using Cinemachine;
using QuantumWeavers.Components.Core;
using DG.Tweening;
using UnityEditor.Animations;

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
        [Tooltip("Text that shows the FOV.")]
        [SerializeField] private TextMeshProUGUI FOVText;
        [SerializeField] private PlayerManager Player;
        [Tooltip("Range of sensibility the player can select from.")]
        private readonly Vector2 _rangeOfSensitivity = new Vector2(1, 50);
        [Tooltip("Range of FOV the player can select from.")]
        private readonly Vector2 _rangeOfFOV = new Vector2(80, 120);

        [SerializeField] private CinemachineVirtualCamera camera;

        [SerializeField] private Animator animator;

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
        public void OnSensitivitySlider()
        {
            Player.SetMouseSensitivity(Mathf.FloorToInt(MouseSensitivitySlider.value * _rangeOfSensitivity.y / _rangeOfSensitivity.x));
            MouseSensitivityText.text = Player.GetMouseSensitivity().ToString();
        }

        /// <summary>
        /// Its sets the FOV to a value equivalent to the value of the slider transformed to de _rangeOfFOV.
        /// </summary>
        /// <param name="value"></param>
        public void OnFOVSlider(float value)
        {
            camera.m_Lens.FieldOfView = Mathf.FloorToInt(_rangeOfFOV.x + (_rangeOfFOV.y-_rangeOfFOV.x) * value);
            FOVText.text = camera.m_Lens.FieldOfView.ToString();
        }

        public void OnExitButton()
        {
            Debug.Log("Salir");
            SoundManager.Instance.Play("MainMenuMusic");
            SoundManager.Instance.Play("Button");
            SceneManager.LoadScene(0);
        }

        public void SetPause(bool pause)
        {
            GameManager.Instance.SetGameStates(pause);
        }
        
        #endregion


    }
}

