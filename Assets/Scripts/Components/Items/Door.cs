using DG.Tweening;
using UnityEngine;

namespace QuantumWeavers.Components.Items {
	public class Door : Interactable {
		[SerializeField] private float OpeningLenght;

		protected override void Interact() {
			transform.DORotate(new Vector3(0, -90, 0), OpeningLenght).SetEase(Ease.InOutFlash);
			InteractableUsed = true;
			ItemToUse.Used -= Interact;
		}
	}
}
