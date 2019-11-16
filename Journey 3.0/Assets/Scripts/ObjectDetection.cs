using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    [SerializeField] List <GameObject> _items = new List<GameObject>();

    public List<GameObject> Items
    {
        get => _items;
        set => _items = value;
    }
    

    private void OnTriggerEnter(Collider other)
    {

        IInteractible itemToPickUp = other.GetComponent<IInteractible>();

        if (itemToPickUp != null && !_items.Contains(other.gameObject))
        {
            _items.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (_items.Contains(other.gameObject))
        {
            _items.Remove(other.gameObject);
        }
    }
}


