using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMiddleTrigger : MonoBehaviour
{
    [SerializeField] private bool playerInMiddleTrigger;

    public bool PlayerInMiddleTrigger
    {
        get => playerInMiddleTrigger;
        set => playerInMiddleTrigger = value;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInMiddleTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInMiddleTrigger = false;
        }
    }
}
