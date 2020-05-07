﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonReader : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler, IDeselectHandler
{
    [SerializeField] private Animator _animator;
    
    // When highlighted with mouse.
    public void OnPointerEnter(PointerEventData eventData)
    {
        //ChangeSelection to be the hover over button
        EventSystem.current.SetSelectedGameObject(gameObject);
        print("Play Animation on Pointer");
        _animator.SetBool("Hover", true);
    }
    
    //When un-highlighted with mouse
    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetBool("Hover", false);
    }

    // When selected.
    public void OnSelect(BaseEventData eventData)
    {
        print("Play Animation on Select");
        _animator.SetBool("Hover", true);
    }

    //When un-selected
    public void OnDeselect(BaseEventData eventData)
    {
        //turn off highlighted buttons
        GetComponent<Selectable>().OnPointerExit(null);
        _animator.SetBool("Hover", false);
    }
}