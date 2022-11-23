using DG.Tweening;
using UnityEngine;

namespace QuantumWeavers.Components.Items {
    public class ButtonDoor : Interactable {
        [Tooltip("Button which is needed to press in order to open the door")] 
        [SerializeField] private Button DoorButton;
        [Tooltip("Time to complete the door's opening")]
        [SerializeField] private float OpenLenght;

        private bool _doorOpen;
        
        private void Start() {
            DoorButton.Pressed += Interact;
        }

        protected override void Interact() {
            if(!_doorOpen) 
                transform.DORotate(new Vector3(0, -90, 0), OpenLenght).SetEase(Ease.InOutFlash);
            else 
                transform.DORotate(new Vector3(0, 0, 0), OpenLenght).SetEase(Ease.InOutFlash);

            _doorOpen = !_doorOpen;
        }
    }
}
