using System;
using UnityEngine;

namespace QuantumWeavers.Player {
    public class PlayerManager : MonoBehaviour {
        private PlayerLocomotion _locomotion;

        private void OnEnable() {
            _locomotion = GetComponent<PlayerLocomotion>();
        }

        private void Update() {
           
        }
    }
}
