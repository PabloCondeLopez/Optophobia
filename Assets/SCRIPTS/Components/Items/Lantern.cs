using System;
using QuantumWeavers.Components.Core;
using UnityEngine;

namespace QuantumWeavers.Components.Items {
	public class Lantern : TakeableItem {
		private InputHandler _input;
		private Light _lanternLight;
		private bool _turnOn;

		protected override void OnEnable() {
			_lanternLight = GetComponentInChildren<Light>();
			_lanternLight.intensity = 0;
			_turnOn = false;

			_input = GameManager.Instance.Input;
			
			base.OnEnable();
		}

		private void Update() {
			if (!_input.OnLanternTurn() || !IsOnHand) return;
			
			UseObject(null);
		}

		public override void UseObject(ObjectDoor interactableObject) {
			_turnOn = !_turnOn;
			
			_lanternLight.intensity = _turnOn ? 2 : 0;
		}
	}
}
