using System;
using DG.Tweening;
using UnityEngine;
using QuantumWeavers.Components.Sound;

namespace QuantumWeavers.Components.Items {
    public class Button : ItemComponent {

        [Tooltip("Event that happens when the button is pressed")]
        public event Action Pressed;

        [SerializeField] 
        private Transform ButtonModel;

        private bool _isPressed;

        /// <summary>
        /// It warns the subscribers to realize the subscribed method.
        /// </summary>
        public void PressButton() {
            Pressed?.Invoke();

            if (!_isPressed) {
                ButtonModel.DOLocalMove(new Vector3(0f, 0f, -0.2f), 1f);
                _isPressed = true;
            }
            else {
                ButtonModel.DOLocalMove(new Vector3(0f, 0f, 0f), 1f);
                _isPressed = false;
            }
            
            SoundManager.Instance.Play("Button");
        }
    }
}
