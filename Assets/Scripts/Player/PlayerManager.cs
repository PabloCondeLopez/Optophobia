using System;
using QuantumWeavers.Core;
using UnityEngine;

namespace QuantumWeavers.Player {
    public class PlayerManager : MonoBehaviour {
        private PlayerLocomotion _locomotion;
        public GameManager GameManager { get; private set; }

        private void OnEnable() {
            _locomotion = GetComponent<PlayerLocomotion>();
            GameManager = GameManager.Instance;
        }

        private void Update() {
           
        }
    }
}
