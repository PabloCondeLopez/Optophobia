using System;
using UnityEngine;

namespace QuantumWeavers.Components.Core {
	public class TriggerManager : MonoBehaviour {
		public static TriggerManager Instance;

		private void Awake() {
			if (Instance != null) return;

			Instance = this;
		}

		[SerializeField] private GameObject Shadow;

		public GameObject GetShadow() { return Shadow; }
	}
}
