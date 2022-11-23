using QuantumWeavers.Components.Sound;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace QuantumWeavers.Components.UI
{
    public class Settings : MonoBehaviour
    {
        [Tooltip("AudioMixer.")]
        public AudioMixer AudioMixer;

        [Tooltip("True - full screen, false - not full screen.")]
        public bool IsFullScreen = true;

        [Tooltip("Slider to change the general volume.")]
        public Slider GeneralSlider;
        [Tooltip("Slider to change the music volume.")]
        public Slider MusicSlider;
        [Tooltip("Slider to change the sound effects volume.")]
        public Slider EffectsSlider;

        [Tooltip("Text that shows the general volume value.")]
        [SerializeField] private TextMeshProUGUI GeneralVolumeText;
        [Tooltip("Text that shows the music volume value.")]
        [SerializeField] private TextMeshProUGUI MusicText;
        [Tooltip("Text that shows the sound effects volume value.")]
        [SerializeField] private TextMeshProUGUI SoundEffectsText;

        #region Unity Methods

        /// <summary>
        /// When started, it calls for StartSounds() to initialize the values.
        /// </summary>
        private void Start()
        {
            StartSounds();
            
        }
        
        private void Update()
        {
            UpdateText();
        }

        #endregion

        #region Methods

        /// <summary>
        /// It initializes all the volumes, the sliders and the texts.
        /// To a preset value if the haven't been changed previously.
        /// Or to the saved values if they have been changed.
        /// </summary>
        protected void StartSounds()
        {
            AudioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("GeneralVolume"));
            AudioMixer.SetFloat("Music", PlayerPrefs.GetFloat("MusicVolume"));
            AudioMixer.SetFloat("SoundEffects", PlayerPrefs.GetFloat("SoundEffectsVolume"));

            GeneralSlider.value = (PlayerPrefs.GetFloat("GeneralVolume") + 40) / 50;
            MusicSlider.value = (PlayerPrefs.GetFloat("MusicVolume") + 40) / 50;
            EffectsSlider.value = (PlayerPrefs.GetFloat("SoundEffectsVolume") + 40) / 50;

            UpdateText();
        }

        /// <summary>
        /// Updates the texts so the reflect the value of the volumes.
        /// </summary>
        protected void UpdateText()
        {
            GeneralVolumeText.text = Mathf.FloorToInt((GeneralSlider.value * 100)).ToString();
            MusicText.text = Mathf.FloorToInt((MusicSlider.value * 100)).ToString();
            SoundEffectsText.text = Mathf.FloorToInt((EffectsSlider.value * 100)).ToString();
        }

        /// <summary>
        /// Changes the screen mode.
        /// </summary>
        /// <param name="fullScreen">Wether the screen is on full screen mode.</param>
        protected void SetFullScreen(bool fullScreen)
        {
            IsFullScreen = fullScreen;
            SoundManager.Instance.Play("Button");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="volume">New value of the general volume.</param>
        protected void SetVolume(float volume)
        {
            AudioMixer.SetFloat("Volume", volume * 50 - 40);
            Debug.Log(volume);
            Debug.Log(volume * 50 - 40);
            PlayerPrefs.SetFloat("GeneralVolume", volume * 50 - 40);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="volume">New value of the music volume.</param>
        protected void SetMusicVolume(float volume)
        {
            AudioMixer.SetFloat("Music", volume * 50 - 40);
            PlayerPrefs.SetFloat("MusicVolume", volume * 50 - 40);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="volume">New value of the sound effects volume.</param>
        protected void SetSoundEffectsVolume(float volume)
        {
            AudioMixer.SetFloat("SoundEffects", volume * 50 - 40);
            PlayerPrefs.SetFloat("SoundEffectsVolume", volume * 50 - 40);
        }


        #endregion
    }

        
}




