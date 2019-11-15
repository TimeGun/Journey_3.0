using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject :  InteractibleObject
{
    private Vector3 _stayPosition;

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    public void GetPickedUp(Transform parent)
    {
        _grabbed = true;
        
        parent.root.GetComponent<InteractWithObject>().SetGrabObject(gameObject);
    }

    public void GetDropped()
    {
        _grabbed = false;
    }

    public override void StartInteraction()
    {
    }
}
