using QuantumWeavers.Components.Core;
using UnityEngine;

namespace QuantumWeavers.Components.Items {
    public class PlayerHand : MonoBehaviour {

        #region _privateVariables

        [Tooltip("Layer applied to the items.")]
        [SerializeField] private LayerMask ItemMask;
        [Tooltip("Radius where the items are going to be detected.")]
        [SerializeField] private float ItemDetectionRadius = 0.5f;
        [Tooltip("Maximum range where the items are going to be detected.")]
        [SerializeField] private float ItemDetectionRange = 1f;
        [Tooltip("Layer applied to the interactable objects.")]
        [SerializeField] private LayerMask InteractableMask;
        [Tooltip("Maximum range where the interactable objects are going to be detected.")]
        [SerializeField] private float InteractableDetectionRange = 1f;

        [Tooltip("GameManager instance.")]
        private GameManager _gameManager;
        [Tooltip("Previous item collider detected.")]
        private Collider _previousItemCollider;
        [Tooltip("Previous interactable collider detected.")]
        private Collider _previousInteractableCollider;
        [Tooltip("Item that the player is holding at the moment.")]
        private TakeableItem _itemOnHand;

        #endregion

        #region Unity Events

        private void Start() {
            _gameManager = GameManager.Instance;
        }

        public void Update() {
            SeekItems();
            SeekInteractableObjects();
        }
        
        #endregion
        
        #region Getters

        /// <summary>
        /// Gets the current item
        /// </summary>
        /// <returns>The item hold on the hand</returns>
        public TakeableItem GetItemOnHand() {
            return _itemOnHand;
        }
        
        #endregion

        #region Setters

        /// <summary>
        /// Replaces the item that the player is holding with the new item.
        /// </summary>
        /// <param name="newItem">Item that you want the player to hold.</param>
        public void SetItemOnHand(TakeableItem newItem) {
            _itemOnHand = newItem;
            _itemOnHand.AttachItem();
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Seeks for items in a sphere
        /// </summary>
        private void SeekItems() {
            if(Physics.SphereCast(transform.position, ItemDetectionRadius, transform.forward, out RaycastHit hit, ItemDetectionRange, ItemMask)) {
                ItemComponent item = hit.collider.GetComponentInParent<ItemComponent>();

                if (_previousItemCollider != hit.collider) {
                    if (hit.collider) {
                        item.OutlineItem();
                        item.HUD().Enable();
                        _previousItemCollider = hit.collider;
                        return;
                    }
                }
                
                if (_gameManager.Input.OnInteract()) {
                    if (item.GetType() == typeof(TakeableItem) || item.GetType().BaseType == typeof(TakeableItem))
                        hit.collider.GetComponentInParent<TakeableItem>().TakeObject();
                    else if (item.GetType() == typeof(Button))
                        hit.collider.GetComponentInParent<Button>().PressButton();
                }
            }

            if(!hit.collider && _previousItemCollider) {
                ItemComponent previousItem = _previousItemCollider.GetComponentInParent<ItemComponent>();
                
                previousItem.RemoveOutline();
                previousItem.HUD().Disable();
                _previousItemCollider = null;
            }
        }

        /// <summary>
        /// Seeks for interactable objects in a range
        /// </summary>
        private void SeekInteractableObjects() {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, InteractableDetectionRange, InteractableMask)) {
                Interactable interactable = hit.collider.GetComponentInParent<Interactable>();

                if (_previousInteractableCollider != hit.collider) {
                    if (hit.collider && interactable) {
                        interactable.OutlineInteractable();
                        _previousInteractableCollider = hit.collider;
                        return;
                    }
                }

                if (_gameManager.Input.OnInteract()) {
                    if(_itemOnHand && interactable is ObjectDoor objectDoor)
                        _itemOnHand.UseObject(objectDoor);
                    else if (interactable is Door door) {
                        door.OpenDoor();
                    }
                }
            }

            if (!hit.collider && _previousInteractableCollider) {
                Interactable previousInteractable = _previousInteractableCollider.GetComponentInParent<Interactable>();
                
                previousInteractable.RemoveOutline();
                _previousInteractableCollider = null;
            }
        }

        #endregion
        
    }
}
