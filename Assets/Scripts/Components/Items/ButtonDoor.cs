using UnityEngine;
using QuantumWeavers.Components.Sound;
namespace QuantumWeavers.Components.Items {
    public class ButtonDoor : Door {

        [Tooltip("Button which is needed to press in order to open the door")] 
        [SerializeField] private Button DoorButton;

        private void Start() {
            DoorButton.Pressed += Interact;
        }

        /// <summary>
        /// It rotates the door from it's hinges.
        /// </summary>
        protected override void Interact() {
            if(!DoorOpen) 
                OpenDoor();
            else 
                CloseDoor();
            
            SoundManager.Instance.Play("Button");
        }
    }
}
