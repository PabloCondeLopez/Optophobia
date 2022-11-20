using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantumWeavers
{
    public class Item : ScriptableObject
    {
        [Tooltip("Name of the item.")]
        public string Name;

        [Tooltip("Position in which the item attatches to the hand.")]
        public Vector3 PosOfAttachment;
    }
}
