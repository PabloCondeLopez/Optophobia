using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public bool isActive { set; get; }
    [SerializeField] private GameObject panel;
    private Image image;
    [SerializeField] private Sprite _selected;
    [SerializeField] private Sprite _nonSelected;
    [SerializeField] private Sprite _selectedScribbles;
    [SerializeField] private Sprite _nonSelectedScribbles;

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = _nonSelected;
    }

    public void OnSelect(BaseEventData eventData) 
    {
        image.sprite = isActive ? _selectedScribbles : _nonSelectedScribbles;
    }
    public void OnDeselect(BaseEventData eventData)
    {
        image.sprite = isActive ? _nonSelectedScribbles : _nonSelected;
    }

    public void ChangeOfState(bool active)
    {
        panel.SetActive(active);
        isActive = active;
    }
}
