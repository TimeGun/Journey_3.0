using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraibleObject : MonoBehaviour, IPickup
{
    [SerializeField] private bool _grabbed;

    private Vector3 _stayPosition;

    void Start()
    {
        
    }


    public void GetPickedUp(Transform parent)
    {
        _grabbed = true;
        parent.root.GetComponent<PickUpObjects>().SetGrabObject(gameObject);
    }

    public void GetDropped()
    {
        _grabbed = false;
    }
}
