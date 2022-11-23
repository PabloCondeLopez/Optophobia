using QuantumWeavers.Classes.Player;
using QuantumWeavers.Components.Core;
using QuantumWeavers.Components.Sound;
using UnityEngine;

namespace QuantumWeavers.Components.Player {
    public class PlayerManager : MonoBehaviour {
        [Tooltip("Position of the camera hold by the character")]
        [SerializeField] private Transform CameraPosition;
        [Tooltip("Player's movement speed")]
        [SerializeField] private float PlayerSpeed = 10f;
        [Tooltip("Sensitivity of the mouse movement")]
        [SerializeField] private float MouseSensitivity = 10f;
        
        // Player locomotion class
        private PlayerLocomotion _locomotion;
        // Player camera class
        private PlayerCamera _camera;

        #region Unity Events

        private void Start() {
            InputHandler input = GameManager.Instance.Input;
            Rigidbody rb = GetComponentInChildren<Rigidbody>();
            Transform modelTransform = transform.GetChild(0).transform;

            _locomotion = new PlayerLocomotion(rb, input, PlayerSpeed, modelTransform);
            _camera = new PlayerCamera(modelTransform, CameraPosition, input, MouseSensitivity);
        }

        private void Update() {
            if (GameManager.Instance.GamePaused) return;
            
            _camera.TickUpdate();
            _locomotion.TickUpdate();
        }
        
        private void OnCollisionEnter(Collision collision) {
            SoundManager.Instance.Play("Button");
        }
        
        #endregion
    }
}
