using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantumWeavers
{
    public class ItemComponent : MonoBehaviour
    {
        private Item Item;

        /// <summary>
        /// 
        /// </summary>
        public void takeObject()
        {
            
        }

        /// <summary>
        /// If the tag of the item correlates to the one of the object, it calls the function unlock() of the interactableObject.
        /// </summary>
        /// <param name="interactableObject">The player is trying to use the item in this object.</param>
        public void UseObject(GameObject interactableObject)
        {
            // comprueba si la etiqueta de interactableObject es la misma que la del objeto
            
            if (interactableObject.CompareTag(tag))
            {
                // usa el objeto => llama a la funcion desbloquear del interactable object
                // actualiza la mano del jugador
            }
        }        
    }
}
