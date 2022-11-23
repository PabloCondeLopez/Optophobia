using System;
using UnityEngine;

namespace QuantumWeavers.Components.Items {
	public abstract class Interactable: MonoBehaviour {
		// Checks if the interactable was already used
		protected bool InteractableUsed;
		// Outline effect applied to the object
		private OutlineEffect _outline;

		#region Unity Events

		private void OnEnable() {
			_outline = GetComponentInChildren<OutlineEffect>();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Interacts with the interactable object
		/// </summary>
		protected virtual void Interact() {
			Debug.Log("To implement interaction on - " + name);
		}
		
		/// <summary>
		/// Shows the object's outline
		/// </summary>
		public void OutlineInteractable() {
			if(!InteractableUsed)
				_outline.OutlineWidth = 10f;
		}

		/// <summary>
		/// Hides the object's outline
		/// </summary>
		public void RemoveOutline() {
			_outline.OutlineWidth = 0f;
		}

		#endregion
		
	}
}
