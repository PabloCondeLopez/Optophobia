using UnityEngine;

namespace QuantumWeavers.Player {
    public class PlayerLocomotion : MonoBehaviour {

        private Rigidbody _rb;

        private void Start() {
            _rb = GetComponent<Rigidbody>();
        }
    }
}
