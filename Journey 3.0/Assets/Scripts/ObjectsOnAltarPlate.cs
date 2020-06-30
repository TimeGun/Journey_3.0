using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsOnAltarPlate : MonoBehaviour
{
    [SerializeField] List <GameObject> _itemsOnAltar = new List<GameObject>();



    public List<GameObject> ItemsOnAltar
    {
        get => _itemsOnAltar;
        set => _itemsOnAltar = value;
    }
    

    private void OnTriggerStay(Collider other)
    {
        if (!other.isTrigger)
        {
            IInteractible itemToPickUp = other.GetComponent<IInteractible>();
            if (itemToPickUp != null && !_itemsOnAltar.Contains(other.gameObject))
            {
                _itemsOnAltar.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger)
        {
            if (_itemsOnAltar.Contains(other.gameObject))
            {
                _itemsOnAltar.Remove(other.gameObject);
            }
        }
    }
}
