using QuantumWeavers.Classes.Player;
using QuantumWeavers.Components.Core;
using QuantumWeavers.Components.Sound;
using UnityEngine;

namespace QuantumWeavers.Components.Player {
    public class PlayerManager : MonoBehaviour {
        [SerializeField] private Transform CameraPosition;
        [SerializeField] private float PlayerSpeed = 10f;
        [SerializeField] private float MouseSensitivity = 10f;

        private PlayerLocomotion _locomotion;
        private PlayerCamera _camera;

        private void Start() {
            InputHandler input = GameManager.Instance.GetInput();
            Rigidbody rb = GetComponentInChildren<Rigidbody>();
            Transform modelTransform = transform.GetChild(0).transform;

            _locomotion = new PlayerLocomotion(rb, input, PlayerSpeed, modelTransform);
            _camera = new PlayerCamera(modelTransform, CameraPosition, input, MouseSensitivity);
        }

        private void Update() {
            _camera.TickUpdate();
            _locomotion.TickUpdate();
        }
        
        private void OnCollisionEnter(Collision collision) {
            SoundManager.Instance.Play("Button");
        }
    }
}
