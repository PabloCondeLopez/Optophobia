using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace QuantumWeavers.Components.UI {
    public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler {
        public void OnPointerEnter(PointerEventData eventData) {
            transform.DOScale(new Vector3(1.3f, 1.3f, 0f), 1f).SetEase(Ease.OutFlash);
        }

        public void OnPointerExit(PointerEventData eventData) {
            transform.DOPause();
            transform.localScale = Vector3.one;
        }

        public void OnSelect(BaseEventData eventData) {
            transform.DOScale(new Vector3(1.3f, 1.3f, 0f), 1f).SetEase(Ease.OutFlash);
        }

        public void OnDeselect(BaseEventData eventData) {
            transform.DOPause();
            transform.localScale = Vector3.one;
        }
    }
}
