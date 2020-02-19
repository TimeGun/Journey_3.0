using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresentInTrigger : MonoBehaviour
{
    [SerializeField] private PlayerInPlacementArea _playerInPlacementArea;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInPlacementArea.PlayerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInPlacementArea.PlayerInTrigger = false;
        }
    }
}
