using System;
using System.Collections;
using DG.Tweening;
using QuantumWeavers.Classes.Player;
using QuantumWeavers.Components.Core;
using QuantumWeavers.Components.Sound;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

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

        [SerializeField] private GameObject Tutorial;
        [SerializeField] private GameObject EyesClosed;
        [SerializeField] private GameObject EyesIndicator;
        [SerializeField] private GameObject DeadPanel;
        [SerializeField] private Animator EyesAnimator;
        [SerializeField] private Animator DeadAnimator;

        private float _deltaDead = 15f;

        private Quaternion _actualTarget = new Quaternion();
        private bool _searchingTarget = false;
        private bool _trigger1Activated = false;
        private bool _onExit = false;
        private float _cameraDelta = 3f;
        private GameObject _shadow;

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
            
            _shadow = TriggerManager.Instance.GetShadow();
        }

        /// <summary>
        /// If the game isn't paused it calls for the camera and locomotion updates.
        /// </summary>
        private void Update() {
            if (GameManager.Instance.GamePaused) return;
            
            _camera.TickUpdate();
            _locomotion.TickUpdate();

            if (!_searchingTarget) return;
            
            Quaternion smoothedRotation = Quaternion.Slerp(CameraPosition.rotation, _actualTarget, 0.003f);
            CameraPosition.rotation = smoothedRotation;
            _cameraDelta -= Time.deltaTime;

            if (CameraPosition.rotation != _actualTarget || _cameraDelta > 0f) return;
            
            _searchingTarget = false;
            _locomotion.SetIsFrozen(false);
            _camera.SetIsFrozen(false);

            _shadow.SetActive(false);
            Tutorial.SetActive(true);

            GameManager.Instance._canCloseEyes = true;
            GameManager.Instance.EyesOpen = true;
            GameManager.Instance.SetGameStates(false);
            EyesClosed.SetActive(true);
            EyesIndicator.SetActive(true);

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
            if(other.gameObject.CompareTag("TriggerFirstEvent") && !_trigger1Activated) {
                _camera.SetIsFrozen(true);
                _locomotion.SetIsFrozen(true);
                _actualTarget = Quaternion.LookRotation(other.transform.GetChild(0).position - CameraPosition.position);
                _searchingTarget = true;
                _trigger1Activated = true;
                
                _shadow.SetActive(true);
                _shadow.transform.DORotate(new Vector3(0, 0, -39f), 2f).SetDelay(0.5f).OnComplete(() => {
                    _shadow.transform.DORotate(new Vector3(0, 0, 0), 2f);
                });
            }
            else if (other.gameObject.CompareTag("TriggerBathroom")) {
                GameManager.Instance.LightsBlink(0.5f, 10);
                SoundManager.Instance.Play("FlickeringLights");
            }
            else if (other.gameObject.CompareTag("TriggerKnock")) {
                if (_onExit && Random.Range(0f, 1f) <= 0.5f) {
                    SoundManager.Instance.Play("Knock");
                }
                _onExit = !_onExit;
            } else if (other.gameObject.CompareTag("TriggerDead")) {
                SoundManager.Instance.Play("TensionMusicDark");
            }
        }

        private void OnTriggerExit(Collider other) {
            if (!other.gameObject.CompareTag("TriggerDead")) return;

            _deltaDead -= Time.deltaTime;

            if (_deltaDead > 0) return;
            
            DeadAnimator.gameObject.SetActive(true);
            DeadAnimator.Play("ManoIzq");

            DeadPanel.SetActive(true);
            EyesAnimator.Play("Blink");
            GameManager.Instance.SetGameStates(false);
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
