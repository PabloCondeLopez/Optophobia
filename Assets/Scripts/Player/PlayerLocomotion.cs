using UnityEngine;

namespace QuantumWeavers.Player {
    public class PlayerLocomotion : MonoBehaviour {
        [SerializeField] private float PlayerSpeed;
        
        private Rigidbody _rb;
        private CapsuleCollider _collider;

        private void OnEnable() {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
        }

        public void TickUpdate() {
            _rb.MovePosition(_rb.position + PlayerSpeed * Time.deltaTime * Vector3.forward);
        }
    }
}
