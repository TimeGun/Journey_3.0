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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        IPickup itemToPickUp = other.GetComponent<IPickup>();

        if (itemToPickUp != null)
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


