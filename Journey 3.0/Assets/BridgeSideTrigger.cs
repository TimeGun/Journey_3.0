using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSideTrigger : MonoBehaviour
{
    [SerializeField] private bool playerInThisTrigger;

    public bool PlayerInThisTrigger
    {
        get => playerInThisTrigger;
        set => playerInThisTrigger = value;
    }

    public GameObject PlankPlacedOnSide
    {
        get => plankPlacedOnSide;
        set => plankPlacedOnSide = value;
    }

    public bool PlankPlaceDown
    {
        get => plankPlaceDown;
        set => plankPlaceDown = value;
    }


    [SerializeField] private GameObject plankPlacedOnSide;

    [SerializeField] private bool plankPlaceDown;

    [SerializeField] private GameObject centerOfPlankObject;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInThisTrigger = true;
            other.SendMessage("ChangeInPlacementBool", true);
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
}
