using UnityEngine;

namespace QuantumWeavers.Items {
    public class PlayerHand : MonoBehaviour {
        [Tooltip("Item that the player is holding at the moment.")]
        private Item _itemOnHand;

        /// <returns>The item that the player is holding.</returns>
        public Item GetItemOnHand() {
            return _itemOnHand;
        }

        /// <summary>
        /// Replaces the item that the player is holding with the new item.
        /// </summary>
        /// <param name="newItem">Item that you want the player to hold.</param>
        public void SetItemOnHand(Item newItem) {
            _itemOnHand = newItem;
            // TODO - se attachea al modelo
        }
    }
}
