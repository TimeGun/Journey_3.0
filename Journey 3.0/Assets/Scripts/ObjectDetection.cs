using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    [SerializeField] List <GameObject> _items = new List<GameObject>();
    
    public GameObject carryingObject;

    public bool checkForObjects = true;
    


    public List<GameObject> Items
    {
        get => _items;
    }
    
    
    private void OnTriggerStay(Collider other)
    {
        if (checkForObjects)
        {
            IInteractible itemToPickUp = other.GetComponent<IInteractible>();
            if (itemToPickUp != null && !_items.Contains(other.gameObject) && itemToPickUp.getGameObject().activeSelf)
            {
                _items.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_items.Contains(other.gameObject) && other.gameObject != carryingObject)
        {
            _items.Remove(other.gameObject);
            
            Material mat = other.GetComponentInChildren<Renderer>().material;

            if (mat != null && mat.HasProperty("_interactible"))
            {
                mat.SetFloat("_interactible", 0);
            }
        }
    }

    public void ClearList()
    {
        _items.Clear();
        checkForObjects = false;
    }
}


