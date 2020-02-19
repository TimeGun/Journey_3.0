using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInPlacementArea : MonoBehaviour
{
    [SerializeField] private bool _playerInTrigger;
    
    //To check if the player is in the plank placement area
    public bool PlayerInTrigger
    {
        get => _playerInTrigger;
        set => _playerInTrigger = value;
    }
}
