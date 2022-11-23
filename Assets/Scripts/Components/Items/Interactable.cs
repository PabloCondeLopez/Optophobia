using UnityEngine;

namespace QuantumWeavers.Components.Items {
	public abstract class Interactable: MonoBehaviour {
		[Tooltip("Item which is needed to use this interactable")]
		[SerializeField] protected ItemComponent ItemToUse;
		
		// Checks if the interactable was already used
		protected bool InteractableUsed;
		// Outline effect applied to the object
		private OutlineEffect _outline;

		#region Unity Events
		
		protected virtual void Start() {
			_outline = GetComponentInChildren<OutlineEffect>();
			
			ItemToUse.Used += Interact;
		}

		#endregion

		#region Getters
		
		/// <summary>
		/// Gets the item which is needed to use this interactable
		/// </summary>
		/// <returns>An item component necessary to use this interactable</returns>
		public ItemComponent GetItemToUse() {
			return ItemToUse;
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
