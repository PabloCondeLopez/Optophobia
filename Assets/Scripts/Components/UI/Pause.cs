using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace QuantumWeavers.Components.UI
{
   

    public class Pause : Settings
    {
 
        [Tooltip("Default settings")]
        public Button DefaultSettingsButton;

        public void OnOpen()
        {

            if (DefaultSettingsButton != null) DefaultSettingsButton.onClick.Invoke();
        }

        private void Start()
        {
            StartSounds();
        }

        protected void LoadSettings()
        {
            /*
            GeneralVolume = settings.GeneralVolume;
            MusicVolume = settings.MusicVolume;
            SoundEffectsVolume = settings.SoundEffectsVolume;
            AudioMixer = settings.AudioMixer;

            IsFullScreen = settings.IsFullScreen;

            GeneralSlider = settings.GeneralSlider;
            MusicSlider = settings.MusicSlider;
            EffectsSlider = settings.EffectsSlider;
            */

            
        }


    }

    
}

