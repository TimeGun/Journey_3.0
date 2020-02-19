using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresentInTrigger : MonoBehaviour
{
    [SerializeField] private PlayerInPlacementArea _playerInPlacementArea;
    
    //player has entered the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInPlacementArea.PlayerInTrigger = true;
        }
    }
    
    //Player has left the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInPlacementArea.PlayerInTrigger = false;
        }
    }
}
