using UnityEngine;
using QuantumWeavers.Classes.Items;

namespace QuantumWeavers.Components.Items
{
    public class ItemComponent : MonoBehaviour
    {
        [Tooltip("Item.")]
        [SerializeField] private Item Item;
        
        //Hand that holds the item. SerializeField: you can change it from the editor
        private PlayerHand _playerHand;
        // HUD of the item
        private ItemHUD _hud;
        private Renderer _itemMaterial;
        private static readonly int Outline = Shader.PropertyToID("_Outline");

        /// <summary>
        /// Finds the PlayerHand and assigns it to _playerHand.
        /// </summary>
        private void OnEnable()
        {
            _playerHand = FindObjectOfType<PlayerHand>();
            _hud = GetComponentInChildren<ItemHUD>(true);
            _itemMaterial = GetComponent<Renderer>();
        }

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

        public void AttachItem() {
            transform.parent = _playerHand.transform;
            transform.localPosition = Item.PosOfAttachment;
        }

        /// <summary>
        /// If the tag of the item correlates to the one of the object, it calls the function unlock() of the interactableObject.
        /// </summary>
        /// <param name="interactableObject">The player is trying to use the item in this object.</param>
        public void UseObject(GameObject interactableObject)
        {
            // TODO - comprueba si la etiqueta de interactableObject es la misma que la del objeto
            
            if (interactableObject.CompareTag(tag))
            {
                // TODO - usa el objeto => llama a la funcion desbloquear del interactable object
                // TODO - actualiza la mano del jugador
            }
        }

        public ItemHUD HUD() {
            return _hud;
        }

        public void OutlineItem() {
            _itemMaterial.material.SetFloat(Outline, 0.01f);
        }

        public void RemoveOutline() {
            _itemMaterial.material.SetFloat(Outline, 0f);
        }
    }
}
