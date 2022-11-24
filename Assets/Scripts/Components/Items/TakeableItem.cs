using System;
using UnityEngine;
using QuantumWeavers.Components.Sound;

namespace QuantumWeavers.Components.Items
{
    public class TakeableItem : ItemComponent
    {
        [Tooltip("Hand that holds the item. SerializeField: you can change it from the editor.")]
        private PlayerHand _playerHand;

        [Tooltip("Event used to use the item.")]
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
                _playerHand.GetItemOnHand().transform.rotation = Quaternion.Euler(Vector3.zero);
            }

            _playerHand.SetItemOnHand(this);
            HUD().Disable();
            RemoveOutline();

            SoundManager.Instance.Play("TakeObject");
        }

        /// <summary>
        /// Attaches the item to the player's hand
        /// </summary>
        public void AttachItem() {
            transform.parent = _playerHand.transform;
            transform.localPosition = ItemInfo.AttachmentPosition;
            transform.localRotation = Quaternion.Euler(ItemInfo.AttachmentRotation);
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
