using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace QuantumWeavers.Sound 
{
    [System.Serializable]
    public class Sound
    {
        public enum AudioTypes { SoundEffect, Music }
        [Tooltip("Types of audio: 0-SoundEffect, 1-Music.")]
        public AudioTypes AudioType;

        [Tooltip("Source of the sound clip.")]
        [HideInInspector] public AudioSource Source;

        [Tooltip("Name of the sound clip.")]
        public string ClipName;

        public AudioClip AudioClip;

        [Tooltip("If the sound loops.")]
        public bool IsLoop;

        [Tooltip("If the sound plays when it's initialized.")]
        public bool PlayOnAwake;

        [Tooltip("Volume of the sound.")]
        [Range(0, 1)] public float Volume = 0.5f;
    }
}
