using System;
using UnityEngine;
using QuantumWeavers.Components.Sound;

namespace QuantumWeavers.Components.Items {
    public class Button : ItemComponent {

        [Tooltip("Event that happens when the button is pressed")]
        public event Action Pressed;

        /// <summary>
        /// It warns the subscribers to realize the subscribed method.
        /// </summary>
        public void PressButton() {
            Pressed?.Invoke();
            SoundManager.Instance.Play("Button");
        }
    }
}
