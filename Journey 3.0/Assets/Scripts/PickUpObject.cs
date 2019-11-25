using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class PickUpObject : MonoBehaviour, IInteractible
{
    private Vector3 _stayPosition;
    
    private Quaternion _stayRotation;

    private Transform _hand;

    private bool _carried;

    public bool Carried
    {
        get => _carried;
    }


    private Rigidbody _rb;

    private Collider _col;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (_carried)
        {
            _stayPosition = _hand.position;
            _stayRotation = _hand.rotation;
            StayInPosition();
            StayInRotation();
        }

    }

    private void StayInRotation()
    {
        transform.rotation = _stayRotation;
    }

    private void StayInPosition()
    {
        transform.position = _stayPosition;
    }


    public void StopInteraction()
    {
        _hand.root.GetComponent<PlayerMovement>().carryingObject = false;

        _col.enabled = true;
        _rb.isKinematic = false;
        _carried = false;
    }
    
    public GameObject getGameObject()
    {
        return gameObject;
    }




    public void StartInteraction(Transform parent)
    {
        _col.enabled = false;
        _rb.isKinematic = true;

        _hand = parent;

        _hand.root.GetComponent<PlayerMovement>().carryingObject = true;
        
        _carried = true;
    }
}
