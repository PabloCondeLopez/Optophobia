using System;
using UnityEngine;
using QuantumWeavers.Classes.Items;
using QuantumWeavers.Components.UI;

namespace QuantumWeavers.Components.Items
{
    public class ItemComponent : MonoBehaviour
    {
        [Tooltip("Item information.")]
        [SerializeField] private Item Item;
        
        //Hand that holds the item. SerializeField: you can change it from the editor
        private PlayerHand _playerHand;
        // HUD of the item
        private ItemHUD _hud;
        // Outline effect of the item
        private OutlineEffect _outline;
        // Event used to use the item
        public event Action Used;

        #region Unity Events

        /// <summary>
        /// Finds the PlayerHand and assigns it to _playerHand.
        /// </summary>
        private void OnEnable()
        {
            _playerHand = FindObjectOfType<PlayerHand>();
            _hud = GetComponentInChildren<ItemHUD>(true);
            _outline = GetComponent<OutlineEffect>();
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
            return Item.Name;
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// The player takes the item interacted with. 
        /// If it already has an item it leaves it in the position of the item that's been collected. 
        /// </summary>
        public void TakeObject()
        {
            if (_playerHand.GetItemOnHand()) {
                _playerHand.GetItemOnHand().transform.parent = transform.parent;
                _playerHand.GetItemOnHand().transform.position = transform.position;
            }

            _playerHand.SetItemOnHand(this);
            _hud.Disable();
            RemoveOutline();
        }

        /// <summary>
        /// Attaches the item to the player's hand
        /// </summary>
        public void AttachItem() {
            transform.parent = _playerHand.transform;
            transform.localPosition = Item.PosOfAttachment;
        }

        /// <summary>
        /// If the tag of the item correlates to the one of the object, tries to use the interactableObject.
        /// </summary>
        /// <param name="interactableObject">The player is trying to use the item in this object.</param>
        public void UseObject(Interactable interactableObject)
        {
            if (interactableObject.GetItemToUse() == this) {
                Used?.Invoke();
                Destroy(gameObject);
            }
        }
        
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
