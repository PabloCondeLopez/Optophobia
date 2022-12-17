using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler {
    
    public bool IsActive;
    [SerializeField] private GameObject Panel;
    private Image _image;
    [SerializeField] private Sprite Selected;
    [SerializeField] private Sprite NonSelected;
    [SerializeField] private Sprite SelectedScribbles;
    [SerializeField] private Sprite NonSelectedScribbles;

    private void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = IsActive ? NonSelectedScribbles : NonSelected;
    }

    public void OnPointerEnter(PointerEventData data) {
        _image.sprite = IsActive ? SelectedScribbles : Selected;
    }
    
    public void OnPointerExit(PointerEventData data) {
        _image.sprite = IsActive ? NonSelectedScribbles : NonSelected;
    }

    public void OnSelect(BaseEventData eventData) 
    {
        _image.sprite = IsActive ? SelectedScribbles : Selected;
    }
    
    public void OnDeselect(BaseEventData eventData)
    {
        _image.sprite = IsActive ? NonSelectedScribbles : NonSelected;
    }

    public void ChangeOfState(bool active)
    {
        Panel.SetActive(active);
        IsActive = active;
        _image.sprite = IsActive ? SelectedScribbles : NonSelected;
    }
}
