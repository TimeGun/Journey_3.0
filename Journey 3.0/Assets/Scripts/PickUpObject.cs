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
    

    [SerializeField] private ListOfIKSettings _listOfIkSettings;

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

    private void LateUpdate()
    {
        if (_carried)
        {
            _stayPosition = _hand.position;
            _stayRotation = _hand.rotation;
            StayInPosition();
            StayInRotation();
        }

    }

    public void StayInRotation()
    {
        transform.rotation = _stayRotation;
    }

    public void StayInPosition()
    {
        transform.position = _stayPosition;
    }


    public void StopInteraction()
    {
        _hand.root.GetComponent<PlayerMovement>().carryingObject = false;
        
        _hand.root.GetComponent<ObjectDetection>().carryingObject = null;        
        _hand.root.GetComponent<InterestFinder>().interactingObj = null;

        

        _col.isTrigger = false;
        _rb.isKinematic = false;
        _carried = false;
        
        RightArmIK.Instance.StopIK();
    }
    
    public GameObject getGameObject()
    {
        return gameObject;
    }

    public bool isActive()
    {
        return _carried;
    }


    public void StartInteraction(Transform parent)
    {
        _col.isTrigger = true;

        _rb.isKinematic = true;

        _hand = parent;

        _hand.root.GetComponent<PlayerMovement>().carryingObject = true;
        _hand.root.GetComponent<ObjectDetection>().carryingObject = gameObject;
        _hand.root.GetComponent<InterestFinder>().interactingObj = gameObject;

        
        _carried = true;

        if (_listOfIkSettings == null)
        {
            _listOfIkSettings = _hand.root.GetComponent<PlayerIKPositions>()._settings;
        }

        RightArmIK.Instance.SetIkTargetAndHint(_listOfIkSettings._premadeIKSettings[0]);
        
    }
}
