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
        [Tooltip("Value of the general volume. From 10 to -40.")]
        public float GeneralVolume;
        [Tooltip("Value of the music volume. From 10 to -40.")]
        public float MusicVolume;
        [Tooltip("Value of the sound effects volume. From 10 to -40.")]
        public float SoundEffectsVolume;
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
        [SerializeField] private TextMeshProUGUI _generalVolumeText;
        [Tooltip("Text that shows the music volume value.")]
        [SerializeField] private TextMeshProUGUI _musicText;
        [Tooltip("Text that shows the sound effects volume value.")]
        [SerializeField] private TextMeshProUGUI _soundEffectsText;

        /// <summary>
        /// When started, it calls for StartSounds() to initialize the values.
        /// </summary>
        private void Start()
        {
            StartSounds();
        }

        /// <summary>
        /// It initializes all the volumes, the sliders and the texts.
        /// </summary>
        private void StartSounds()
        {
            GeneralVolume = 10;
            MusicVolume = 10;
            SoundEffectsVolume = 10;
            SoundManager.Instance.UpdateMixerVolume(MusicVolume, SoundEffectsVolume);

            GeneralSlider.value = GeneralVolume;
            MusicSlider.value = MusicVolume;
            EffectsSlider.value = SoundEffectsVolume;

            UpdateText();
        }

        /// <summary>
        /// Calls for UpdateText() so the text always reflects the value of the volumes.
        /// </summary>
        private void Update()
        {
            UpdateText();
        }

        /// <summary>
        /// Updates the texts so the reflect the value of the volumes.
        /// </summary>
        private void UpdateText()
        {
            _generalVolumeText.text = Mathf.FloorToInt((GeneralSlider.value * 100)).ToString();
            _musicText.text = Mathf.FloorToInt((MusicSlider.value * 100)).ToString();
            _soundEffectsText.text = Mathf.FloorToInt((EffectsSlider.value * 100)).ToString();
        }

        /// <summary>
        /// Changes the screen mode.
        /// </summary>
        /// <param name="fullScreen">Wether the screen is on full screen mode.</param>
        public void SetFullScreen(bool fullScreen)
        {
            IsFullScreen = fullScreen;
            SoundManager.Instance.Play("Button");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="volume">New value of the general volume.</param>
        public void SetVolume(float volume)
        {
            GeneralVolume = volume * 100 / 2 - 40;
            AudioMixer.SetFloat("Volume", GeneralVolume);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="volume">New value of the music volume.</param>
        public void SetMusicVolume(float volume)
        {
            MusicVolume = volume * 100 / 2 - 40;
            SoundManager.Instance.UpdateMixerVolume(MusicVolume, SoundEffectsVolume);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="volume">New value of the sound effects volume.</param>
        public void SetSoundEffectsVolume(float volume)
        {
            SoundEffectsVolume = volume * 100 / 2 - 40;
            SoundManager.Instance.UpdateMixerVolume(MusicVolume, SoundEffectsVolume);
        }


    }
}

