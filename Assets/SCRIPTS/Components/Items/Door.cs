using DG.Tweening;
using UnityEngine;

namespace QuantumWeavers.Components.Items {
	public class Door : Interactable {
		[Tooltip("Time to complete the door's opening")]
		[SerializeField] protected float OpeningLenght;
		[Tooltip("Angle to open the door")] 
		[SerializeField] protected float Angle;
		[Tooltip("Door is open")]
		protected bool DoorOpen;

		public void OpenDoor() {
			transform.DORotate(new Vector3(0, Angle, 0), OpeningLenght).SetEase(Ease.InOutFlash);
			InteractableUsed = true;
			DoorOpen = true;
		}

		public void CloseDoor() {
			transform.DORotate(new Vector3(0, 0, 0), OpeningLenght).SetEase(Ease.InOutFlash);
			InteractableUsed = false;
			DoorOpen = false;
		}
	}
}
