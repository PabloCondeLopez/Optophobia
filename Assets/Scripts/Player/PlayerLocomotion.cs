using UnityEngine;
using UnityEngine.InputSystem;

namespace QuantumWeavers.Player {
    public class PlayerLocomotion : MonoBehaviour {
        [SerializeField] private float PlayerSpeed;
        [SerializeField] private GameObject CineMachineCameraTarget;
        [SerializeField] private float TopClamp = 90.0f;
        [SerializeField] private float BottomClamp = -90.0f;

        private Rigidbody _rb;
        private CapsuleCollider _collider;
        private PlayerInput _input;

        private float _cinemachineTargetPitch;
        
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        private void OnEnable() {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
            _input = GetComponent<PlayerInput>();
        }

        public void TickUpdate() {
            //_rb.MovePosition(_rb.position + PlayerSpeed * Time.deltaTime * Vector3.forward);
        }

        public void LateTickUpdate() {
            CameraRotation();
        }

        private void CameraRotation() {
            
        }
    }
}
