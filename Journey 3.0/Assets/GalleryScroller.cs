using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GalleryScroller : MonoBehaviour
{
    [SerializeField] private int pageNumber = 0;
    
    [SerializeField] private UIPage[] _pages;

    public void ScrollUp()
    {
        if (pageNumber == 1)
        {
            _pages[0].gameObject.SetActive(true);
            _pages[1].gameObject.SetActive(false);
            _pages[2].gameObject.SetActive(false);
            
            //SetDefaultEventSystem(_pages[0].defaultSelect);
            pageNumber = 0;
        }else if (pageNumber == 2)
        {
            _pages[0].gameObject.SetActive(false);
            _pages[1].gameObject.SetActive(true);
            _pages[2].gameObject.SetActive(false);
            
            //SetDefaultEventSystem(_pages[1].defaultSelect);
            pageNumber = 1;
        }
    }
    
    public void ScrollDown()
    {
        if (pageNumber == 0)
        {
            _pages[0].gameObject.SetActive(false);
            _pages[1].gameObject.SetActive(true);
            _pages[2].gameObject.SetActive(false);
            
            //SetDefaultEventSystem(_pages[1].defaultSelect);
            pageNumber = 1;
        }else if(pageNumber == 1)
        {
            _pages[0].gameObject.SetActive(false);
            _pages[1].gameObject.SetActive(false);
            _pages[2].gameObject.SetActive(true);
            
            //SetDefaultEventSystem(_pages[2].defaultSelect);
            pageNumber = 2;
        }
    }

    void SetDefaultEventSystem(GameObject obj)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(obj);
    }
}
