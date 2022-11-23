using UnityEngine;

namespace QuantumWeavers.Classes.Items {
    [System.Serializable]
    public class Item
    {
        [Tooltip("Name of the item.")]
        public string Name;
        [Tooltip("Position in which the item attaches to the hand.")]
        public Vector3 PosOfAttachment;
    }
}
