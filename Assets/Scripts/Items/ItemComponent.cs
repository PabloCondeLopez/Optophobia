using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantumWeavers
{
    public class ItemComponent : MonoBehaviour
    {
        [Tooltip("Item.")]
        private Item _item;
        [Tooltip("Hand that holds the item. SerializeField: you can change it from the editor.")]
        [SerializeField] private PlayerHand _playerHand;

        /// <summary>
        /// Finds the PlayerHand and assigns it to _playerHand.
        /// </summary>
        private void OnEnable()
        {
            _playerHand = FindObjectOfType<PlayerHand>();
        }

        /// <summary>
        /// The player takes the item interacted with. 
        /// If it already has an item it leaves it in the position of the item that's been collected. 
        /// </summary>
        public void TakeObject()
        {
            if (_playerHand.GetItemOnHand())
            {
                // TODO - asignarle la posicion del objeto que coges
            }

            _playerHand.SetItemOnHand(_item);
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
    }
}
