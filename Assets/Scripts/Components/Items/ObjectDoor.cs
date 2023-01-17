using System;
using DG.Tweening;
using UnityEngine;

namespace QuantumWeavers.Components.Items {
	public class ObjectDoor : Interactable {

		[Tooltip("Item which is needed to use this interactable")]
		[SerializeField] protected TakeableItem ItemToUse;
		[Tooltip("Time to complete the door's opening")]
		[SerializeField] private float OpeningLenght;

		private bool _doorOpen = false;
		[SerializeField] private int openAngle = -90;

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
			
			transform.DORotate(new Vector3(0, -90, 0), OpeningLenght).SetEase(Ease.InOutFlash);
			InteractableUsed = true;
			ItemToUse.Used -= Interact;
		}
		
		#endregion
	}
}
