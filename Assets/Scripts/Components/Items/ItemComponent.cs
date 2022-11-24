using QuantumWeavers.Classes.Items;
using QuantumWeavers.Components.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace QuantumWeavers.Components.Items {
    public abstract class ItemComponent : MonoBehaviour {

        [Tooltip("Item information.")]
        [SerializeField] protected Item ItemInfo;

        [Tooltip("Outline effect of the item.")]
        private OutlineEffect _outline;
        [Tooltip("HUD of the item.")]
        private ItemHUD _hud;
        
        #region Unity Events
        
        protected virtual void OnEnable() {
            // Outline effect of the item
            _outline = GetComponent<OutlineEffect>();
            _hud = GetComponentInChildren<ItemHUD>(true);
        }

        #endregion

        #region Getters

        /// <summary>
        /// Gets the item's HUD
        /// </summary>
        /// <returns>The item's HUD</returns>
        public ItemHUD HUD() {
            return _hud;
        }
        
        /// <summary>
        /// Gets the name of the item
        /// </summary>
        /// <returns>The name of the item</returns>
        public string ItemName() {
            return ItemInfo.Name;
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Shows the object's outline
        /// </summary>
        public void OutlineItem() {
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
