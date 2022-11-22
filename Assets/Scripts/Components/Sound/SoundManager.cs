using System;
using UnityEngine;
using UnityEngine.Audio;
using QuantumWeavers.Shared;

namespace QuantumWeavers.Components.Sound {
	public class SoundManager : MonoBehaviour {
		[Tooltip("Instance of SoundManager, so it can be accessed from other classes.")]
        public static SoundManager Instance;

        [Tooltip("Group of Music type sounds. SerializeField: you can change it from the editor.")]
        [SerializeField] private AudioMixerGroup MusicMixerGroup;
        [Tooltip("Group of SoundEffect type sounds. SerializeField: you can change it from the editor.")]
        [SerializeField] private AudioMixerGroup SoundEffectsMixerGroup;
        [Tooltip("Array of sounds. SerializeField: you can change it from the editor.")] 
        [SerializeField] private Classes.Sound.Sound[] Sounds;

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


            foreach (Classes.Sound.Sound s in Sounds)
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
            Classes.Sound.Sound s = Array.Find(Sounds, dummySound => dummySound.ClipName == clipName);
            if (s == null) {
                Debug.LogError("Sound " + clipName + " not found");
                return;
            }
            s.Source.Play();
        }

        /// <summary>
        /// Finds the clip named "clipname" and stops it if it exists.
        /// </summary>
        /// <param name="clipName">Name of the clip to Stop.</param>
        public void Stop(string clipName)
        {
            Classes.Sound.Sound s = Array.Find(Sounds, dummySound => dummySound.ClipName == clipName);
            if (s == null) {
                Debug.LogError("Sound " + clipName + " not found");
                return;
            }
            s.Source.Stop();
        }

        /// <summary>
        /// Updates the volume in settings.
        /// </summary>
        public void UpdateMixerVolume(float musicVolume, float soundEffectsVolume)
        {
            MusicMixerGroup.audioMixer.SetFloat("Music", musicVolume);
            SoundEffectsMixerGroup.audioMixer.SetFloat("SoundEffects", soundEffectsVolume);
        }
	}
	
}
