using System;
using DG.Tweening;
using UnityEngine;

namespace QuantumWeavers.Components.Items {
	public class ObjectDoor : Door {

		[Tooltip("Item which is needed to use this interactable")]
		[SerializeField] protected TakeableItem ItemToUse;

		#region Unity Events

		private void Start() {
			ItemToUse.Used += Interact;
		}
		
		#endregion

		#region Getters
		
		/// <summary>
		/// Gets the item which is needed to use this interactable
		/// </summary>
		/// <returns>An item component necessary to use this interactable</returns>
		public TakeableItem GetItemToUse() {
			return ItemToUse;
		}

		#endregion
		
		#region Methods

		/// <summary>
		/// It rotates the door from it's hinges.
		/// </summary>
		protected override void Interact() {
			
			OpenDoor();
			ItemToUse.Used -= Interact;
		}
		
		#endregion
	}
}
