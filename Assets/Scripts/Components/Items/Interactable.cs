using System;
using UnityEngine;

namespace QuantumWeavers.Components.Items {
	public abstract class Interactable: MonoBehaviour {
		[SerializeField] protected ItemComponent ItemToUse;

		protected bool InteractableUsed;
		
		private Renderer _itemMaterial;
		private static readonly int Outline = Shader.PropertyToID("_Outline");

		protected virtual void Start() {
			_itemMaterial = GetComponentInChildren<Renderer>();
			
			ItemToUse.Used += Interact;
		}

		public ItemComponent GetItemToUse() {
			return ItemToUse;
		}
		
		protected virtual void Interact() {
			Debug.Log("To implement interaction on - " + name);
		}
		
		public void OutlineInteractable() {
			if(!InteractableUsed)
				_itemMaterial.material.SetFloat(Outline, 0.01f);
		}

		public void RemoveOutline() {
			_itemMaterial.material.SetFloat(Outline, 0f);
		}
	}
}
