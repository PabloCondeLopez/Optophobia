using System.Collections;
using System.Collections.Generic;
using System;               // para los arrays
using UnityEngine;
using UnityEngine.Audio;

namespace QuantumWeavers.Sound
{
    public class SoundManager : MonoBehaviour
    {
        [Tooltip("Instance of SoundManager, so it can be accessed from other classes.")]
        public static SoundManager Instance;

        [Tooltip("Group of Music type sounds. SerializeField: you can change it from the editor.")]
        [SerializeField] public AudioMixerGroup MusicMixerGroup;
        [Tooltip("Group of SoundEffect type sounds. SerializeField: you can change it from the editor.")]
        [SerializeField] public AudioMixerGroup SoundEffectsMixerGroup;
        [Tooltip("Array of sounds. SerializeField: you can change it from the editor.")]
        [SerializeField] private Sound[] _sounds;

        /// <summary>
        /// Initializes Instance. Calls for it to not be destroyed when loading new scenes.
        /// It initializes each sound in _sounds, establishes their type, an plays them if playOnAwake is true.
        /// </summary>
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);


            foreach (Sound s in _sounds)
            {
                s.Source = gameObject.AddComponent<AudioSource>();
                s.Source.clip = s.AudioClip;
                s.Source.loop = s.IsLoop;
                s.Source.volume = s.Volume;

                switch (s.AudioType)
                {
                    case AudioTypes.SoundEffect:
                        s.Source.outputAudioMixerGroup = SoundEffectsMixerGroup;
                        break;

                    case AudioTypes.Music:
                        s.Source.outputAudioMixerGroup = MusicMixerGroup;
                        break;
                }

                if (s.PlayOnAwake)
                    s.Source.Play();
            }
        }

        /// <summary>
        /// Finds the clip named "clipname" and plays it if it exists.
        /// </summary>
        /// <param name="clipName">Name of the clip to Play.</param>
        public void Play(string clipName)
        {
            Sound s = Array.Find(_sounds, dummySound => dummySound.ClipName == clipName);
            if (s == null) return;
            s.Source.Play();
        }

        /// <summary>
        /// Finds the clip named "clipname" and stops it if it exists.
        /// </summary>
        /// <param name="clipName">Name of the clip to Stop.</param>
        public void Stop(string clipName)
        {
            Sound s = Array.Find(_sounds, dummySound => dummySound.ClipName == clipName);
            if (s == null) return;
            s.Source.Stop();
        }

        /// <summary>
        /// Updates the volume in settings.
        /// </summary>
        public void updateMixerVolume()
        {
            //Debug.Log("ahora el volumen es" + Settings.soundEffectsVolume.ToString());
            //TODO - Create Settings class and connect it to SoundManager.
            //TODO - MusicMixerGroup.audioMixer.SetFloat("Music", Settings.musicVolume);
            //TODO - SoundEffectsMixerGroup.audioMixer.SetFloat("SoundEffects", Settings.soundEffectsVolume);
        }
    }
}

