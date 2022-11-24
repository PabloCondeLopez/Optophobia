using System;
using UnityEngine;

namespace QuantumWeavers.Components.Items {
    public class Button : ItemComponent {

        [Tooltip("Event that happens when the button is pressed")]
        public event Action Pressed;

        /// <summary>
        /// It warns the subscribers to realize the subscribed method.
        /// </summary>
        public void PressButton() {
            Pressed?.Invoke();
        }
    }
}
