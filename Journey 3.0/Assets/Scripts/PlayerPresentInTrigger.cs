using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresentInTrigger : MonoBehaviour
{
    [SerializeField] private PlayerInPlacementArea _playerInPlacementArea;
    [SerializeField] private PlankPlacement _plankPlacement;
    
    //player has entered the trigger
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInPlacementArea.PlayerInTrigger = true;
            other.SendMessage("ChangeInPlacementBool", true);
            other.SendMessage("SetPlacementArea", _plankPlacement);
        }
    }
    
    //Player has left the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInPlacementArea.PlayerInTrigger = false;
            other.SendMessage("ChangeInPlacementBool", false);
        }
    }
}
