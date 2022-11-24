using UnityEngine;

namespace QuantumWeavers.Classes.Items {
    [CreateAssetMenu(fileName = "Item",  menuName = "Items")]
    public class Item : ScriptableObject
    {
        [Tooltip("Name of the item.")]
        public string Name;
        [Tooltip("Position in which the item attaches to the hand.")]
        public Vector3 AttachmentPosition;
        [Tooltip("Rotation which the object will have when attached")]
        public Vector3 AttachmentRotation;
    }
}
