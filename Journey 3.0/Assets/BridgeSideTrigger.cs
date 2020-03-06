using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSideTrigger : MonoBehaviour, IPlaceableArea
{
    [SerializeField] private bool playerInThisTrigger;

    public bool PlayerInThisTrigger
    {
        get => playerInThisTrigger;
        set => playerInThisTrigger = value;
    }

    public GameObject PlankPlacedOnSide
    {
        get => _plankPlacedOnSide;
        set => _plankPlacedOnSide = value;
    }

    public bool PlankPlaceDownBool
    {
        get => _plankPlacedDownBool;
        set => _plankPlacedDownBool = value;
    }


    [SerializeField] private GameObject _plankPlacedOnSide;

    [SerializeField] private bool _plankPlacedDownBool;

    [SerializeField] private GameObject _centerOfPlankObject;

    [SerializeField] private GameObject _plank;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInThisTrigger = true;
            other.SendMessage("ChangeInPlacementBool", true);
            other.SendMessage("SetPlacementArea", this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInThisTrigger = false;
            other.SendMessage("ChangeInPlacementBool", false);
        }
    }


    public GameObject GetCenterObject()
    {
        return _centerOfPlankObject;
    }

    public GameObject GetPlank()
    {
        return _plank;
    }

    public void SetPlank(GameObject value)
    {
        _plank = value;
    }

    public bool GetPlankPlacedDown()
    {
        return _plankPlacedDownBool;
    }

    public void SetPlankPlacedDown(bool value)
    {
        _plankPlacedDownBool = value;
    }

    public bool AdjustPositionBool()
    {
        return true;
    }
}
