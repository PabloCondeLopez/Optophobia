using UnityEngine;

namespace QuantumWeavers.Player {
    public class PlayerLocomotion : MonoBehaviour {

        [SerializeField] private float PlayerSpeed = 10f;
        [SerializeField] private Camera PlayerCamera;

        private PlayerManager _playerManager;
        private Rigidbody _rb;

        private float _xMovement;
        private float _zMovement;

        private void Start() {
            _rb = GetComponent<Rigidbody>();
            _playerManager = GetComponent<PlayerManager>();
        }

        private void Update() {
            HandleMovement();
        }

        private void HandleMovement() {
            _xMovement = _playerManager.GameManager.GetInput().GetMovement().x;
            _zMovement = _playerManager.GameManager.GetInput().GetMovement().y;
            
            if(!_playerManager.GameManager.EyesOpen) {
                Vector3 forward = PlayerCamera.transform.forward.normalized;
                Vector3 right = PlayerCamera.transform.right.normalized;

                forward.y = 0;
                right.y = 0;

                Vector3 forwardRelative = _zMovement * forward;
                Vector3 rightRelative = _xMovement * right;

                Vector3 cameraRelativeMovement = forwardRelative + rightRelative;
                
                _rb.MovePosition(_rb.position + PlayerSpeed * Time.deltaTime * cameraRelativeMovement);
            }
        }
    }
}
