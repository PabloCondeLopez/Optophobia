using QuantumWeavers.Components.Core;
using UnityEngine;

namespace QuantumWeavers.Components.Items {
    public class PlayerHand : MonoBehaviour {
        [SerializeField] private LayerMask ItemMask;
        [SerializeField] private float ItemDetectionRadius = 0.5f;
        [SerializeField] private float ItemDetectionRange = 1f;

        private GameManager _gameManager;
        
        private Collider _previousCollider;
        private static readonly int Outline = Shader.PropertyToID("_Outline");
        
        [Tooltip("Item that the player is holding at the moment.")]
        private ItemComponent _itemOnHand;

        private void Start() {
            _gameManager = GameManager.Instance;
        }

        public void Update() {
            SeekItems();
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
                
                if (_previousCollider != hit.collider) {
                    if (hit.collider) {
                        item.OutlineItem();
                        item.HUD().Enable();
                        _previousCollider = hit.collider;
                        return;
                    }
                }
                
                if (_gameManager.GetInput().OnItemPick()) {
                    item.TakeObject();
                }
            }

            if(!hit.collider && _previousCollider) {
                ItemComponent previousItem = _previousCollider.GetComponent<ItemComponent>();
                
                previousItem.RemoveOutline();
                previousItem.HUD().Disable();
                _previousCollider = null;
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, ItemDetectionRange);
 
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
