using System;
using UnityEngine;

namespace QuantumWeavers.Components.Items {
    public class Button : ItemComponent {
        // Event that happens when the button is pressed
        public event Action Pressed;

        public void PressButton() {
            Pressed?.Invoke();
        }
    }
}
