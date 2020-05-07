using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GalleryScroller : MonoBehaviour
{
    [SerializeField] private int pageNumber = 0;
    
    [SerializeField] private UIPage[] _pages;

    [SerializeField] private Button _upButton;
    [SerializeField] private Button _downButton;

    public void ScrollUp()
    {
        if (pageNumber == 1)
        {
            //Set Page 0 as active
            _pages[0].gameObject.SetActive(true);
            _pages[1].gameObject.SetActive(false);
            _pages[2].gameObject.SetActive(false);
            
            pageNumber = 0;
            _upButton.interactable = false;
            EventSystem.current.SetSelectedGameObject(_downButton.gameObject);
        }else if (pageNumber == 2)
        {
            //Set Page 1 as active
            _pages[0].gameObject.SetActive(false);
            _pages[1].gameObject.SetActive(true);
            _pages[2].gameObject.SetActive(false);
            
            pageNumber = 1;
            
            //Enable Down button
            _downButton.interactable = true;
        }
    }
    
    public void ScrollDown()
    {
        if (pageNumber == 0)
        {
            //Set Page 1 as active
            _pages[0].gameObject.SetActive(false);
            _pages[1].gameObject.SetActive(true);
            _pages[2].gameObject.SetActive(false);
            
            pageNumber = 1;
            
            //Enable Up button
            _upButton.interactable = true;
        }else if(pageNumber == 1)
        {
            //Set Page 2 as active
            _pages[0].gameObject.SetActive(false);
            _pages[1].gameObject.SetActive(false);
            _pages[2].gameObject.SetActive(true);
            
            pageNumber = 2;
            
            //Disable down button since you are at the bottom of the gallery
            _downButton.interactable = false;
            EventSystem.current.SetSelectedGameObject(_upButton.gameObject);
        }
    }

    void SetDefaultEventSystem(GameObject obj)
    {
        //Sets the default ui element as selected on the page
        //not needed at the moment but might be useful later
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(obj);
    }

    private void OnDisable()
    {
        ResetGalleryPage();
    }

    void ResetGalleryPage()
    {
        //Set Page one as active
        _pages[0].gameObject.SetActive(true);
        _pages[1].gameObject.SetActive(false);
        _pages[2].gameObject.SetActive(false);
        
        
        //Re-enable the down button for next gallery opening
        _downButton.interactable = true;
        _upButton.interactable = false;
        
        //Reset Page Number
        pageNumber = 0;
    }
}
