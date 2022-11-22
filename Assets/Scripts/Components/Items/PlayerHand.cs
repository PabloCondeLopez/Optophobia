using QuantumWeavers.Components.Core;
using UnityEngine;

namespace QuantumWeavers.Components.Items {
    public class PlayerHand : MonoBehaviour {
        [SerializeField] private LayerMask ItemMask;
        [SerializeField] private float ItemDetectionRadius = 0.5f;
        [SerializeField] private float ItemDetectionRange = 1f;
        
        [SerializeField] private LayerMask InteractableMask;
        [SerializeField] private float InteractableDetectionRange = 1f;

        private GameManager _gameManager;
        
        private Collider _previousItemCollider;
        private Collider _previousInteractableCollider;

        [Tooltip("Item that the player is holding at the moment.")]
        private ItemComponent _itemOnHand;

        private void Start() {
            _gameManager = GameManager.Instance;
        }

        public void Update() {
            SeekItems();
            SeekInteractables();
        }
        
        /// <summary>
        /// Gets the current item
        /// </summary>
        /// <returns>The item hold on the hand</returns>
        public ItemComponent GetItemOnHand() {
            return _itemOnHand;
        }


        /// <summary>
        /// Replaces the item that the player is holding with the new item.
        /// </summary>
        /// <param name="newItem">Item that you want the player to hold.</param>
        public void SetItemOnHand(ItemComponent newItem) {
            _itemOnHand = newItem;
            _itemOnHand.AttachItem();
        }
        
        private void SeekItems() {
            if(Physics.SphereCast(transform.position, ItemDetectionRadius, transform.forward, out RaycastHit hit, ItemDetectionRange, ItemMask)) {
                ItemComponent item = hit.collider.GetComponent<ItemComponent>();
                
                if (_previousItemCollider != hit.collider) {
                    if (hit.collider) {
                        item.OutlineItem();
                        item.HUD().Enable();
                        _previousItemCollider = hit.collider;
                        return;
                    }
                }
                
                if (_gameManager.GetInput().OnInteract()) {
                    item.TakeObject();
                }
            }

            if(!hit.collider && _previousItemCollider) {
                ItemComponent previousItem = _previousItemCollider.GetComponent<ItemComponent>();
                
                previousItem.RemoveOutline();
                previousItem.HUD().Disable();
                _previousItemCollider = null;
            }
        }

        private void SeekInteractables() {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, InteractableDetectionRange, InteractableMask)) {
                Interactable interactable = hit.collider.GetComponentInParent<Interactable>();

                if (_previousInteractableCollider != hit.collider) {
                    if (hit.collider) {
                        interactable.OutlineInteractable();
                        _previousInteractableCollider = hit.collider;
                        return;
                    }
                }

                if (_gameManager.GetInput().OnInteract() && _itemOnHand) {
                    _itemOnHand.UseObject(interactable);
                }
            }

            if (!hit.collider && _previousInteractableCollider) {
                Interactable previousInteractable = _previousInteractableCollider.GetComponentInParent<Interactable>();
                
                previousInteractable.RemoveOutline();
                _previousInteractableCollider = null;
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, Vector3.forward * InteractableDetectionRange);

            RaycastHit hit;
            if (Physics.SphereCast(transform.position, ItemDetectionRadius, transform.forward * 1, out hit, ItemDetectionRange, ItemMask))
            {
                Gizmos.color = Color.green;
                Vector3 sphereCastMidpoint = transform.position + (transform.forward * hit.distance);
                Gizmos.DrawWireSphere(sphereCastMidpoint, ItemDetectionRadius);
                Gizmos.DrawSphere(hit.point, 0.1f);
                Debug.DrawLine(transform.position, sphereCastMidpoint, Color.green);
            }
            else
            {
                Gizmos.color = Color.red;
                Vector3 sphereCastMidpoint = transform.position + (transform.forward * (ItemDetectionRange-ItemDetectionRadius));
                Gizmos.DrawWireSphere(sphereCastMidpoint, ItemDetectionRadius);
                Debug.DrawLine(transform.position, sphereCastMidpoint, Color.red);
            }
        }
    }
}
