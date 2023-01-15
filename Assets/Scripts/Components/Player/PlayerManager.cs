using System.Collections;
using QuantumWeavers.Classes.Player;
using QuantumWeavers.Components.Core;
using QuantumWeavers.Components.Sound;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QuantumWeavers.Components.Player {
    public class PlayerManager : MonoBehaviour {

        #region _privateVariables

        [Tooltip("Position of the camera hold by the character.")]
        [SerializeField] private Transform CameraPosition;
        [Tooltip("Player's movement speed.")]
        [SerializeField] private float PlayerSpeed = 10f;
        [Tooltip("Sensitivity of the mouse movement.")]
        [SerializeField] private float MouseSensitivity = 10f;

        [Tooltip("Player locomotion class.")]
        private PlayerLocomotion _locomotion;
        [Tooltip("Player camera class.")]
        private PlayerCamera _camera;
        [Tooltip("Gamepad")] 
        private Gamepad _gamepad;

        private Quaternion actualTarget = new Quaternion();
        private bool searchingTarget = false;
        private bool triggerActivated = false;

        #endregion

        #region Unity Events

        /// <summary>
        /// Initializes variables.
        /// </summary>
        private void Start() {
            InputHandler input = GameManager.Instance.Input;
            Rigidbody rb = GetComponentInChildren<Rigidbody>();
            Transform modelTransform = transform.GetChild(0).transform;

            _locomotion = new PlayerLocomotion(rb, input, PlayerSpeed, modelTransform);
            _camera = new PlayerCamera(modelTransform, CameraPosition, input, MouseSensitivity);
            _gamepad = Gamepad.current;
        }

        /// <summary>
        /// If the game isn't paused it calls for the camera and locomotion updates.
        /// </summary>
        private void Update() {
            if (GameManager.Instance.GamePaused) return;
            
            _camera.TickUpdate();
            _locomotion.TickUpdate();

            if (searchingTarget)
            {
                Quaternion smoothedRotation = Quaternion.Lerp(CameraPosition.rotation, actualTarget, 0.005f);
                CameraPosition.rotation = smoothedRotation;
                if(CameraPosition.rotation == actualTarget)
                {
                    searchingTarget = false;
                    _locomotion.SetIsFrozen(false);
                    _camera.SetIsFrozen(false);
                }
            }
        }
        
        /// <summary>
        /// Plays a sound when the player collides with something.
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision) {
            if (collision.collider.CompareTag("Floor")) return;
            
            SoundManager.Instance.Play("Collision");

            StartCoroutine(HandleVibration());
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("TriggerFirstEvent") && !triggerActivated)
            {
                _camera.SetIsFrozen(true);
                _locomotion.SetIsFrozen(true);
                actualTarget = Quaternion.LookRotation(other.transform.GetChild(0).position - CameraPosition.position);
                searchingTarget = true;
                triggerActivated = true;
            }
            else if (other.gameObject.CompareTag("TriggerBathroom")){
                // al llegar al baño parpadean las luces y suena un ruido
                GameManager.Instance.LightsBlink(0.5f, 10);
            }
        }

        #endregion

        #region Getters&Setters

        public float GetMouseSensitivity()
        {
            return MouseSensitivity;
        }

        public void SetMouseSensitivity(float value)
        {
            MouseSensitivity = value;
        }
        
        #endregion

        #region Methods

        private IEnumerator HandleVibration() {
            _gamepad?.SetMotorSpeeds(0.75f, 0.75f);
            yield return new WaitForSeconds(0.75f);
            _gamepad?.SetMotorSpeeds(0f, 0f);
        }

        #endregion
    }
}
