using System;
using UnityEngine;
using QuantumWeavers.Classes.Items;
using QuantumWeavers.Components.UI;
using QuantumWeavers.Components.Sound;

namespace QuantumWeavers.Components.Items
{
    public class TakeableItem : ItemComponent
    {
        //Hand that holds the item. SerializeField: you can change it from the editor
        private PlayerHand _playerHand;
        
        // Event used to use the item
        public event Action Used;

        #region Unity Events
        
        protected override void OnEnable()
        {
            base.OnEnable();
            
            _playerHand = FindObjectOfType<PlayerHand>();
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
            HUD().Disable();
            RemoveOutline();

            SoundManager.Instance.Play("Button");
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
        public void UseObject(ObjectDoor interactableObject)
        {
            if (interactableObject.GetItemToUse() == this) {
                Used?.Invoke();
                Destroy(gameObject);
            }
        }
        
        #endregion
    }
}
