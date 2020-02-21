using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInPlacementArea : MonoBehaviour
{
    [SerializeField] private bool _playerInTrigger;
    [SerializeField] private InterestFinder _playerInterestFinder;

    public InterestFinder PlayerInterestFinder
    {
        get => _playerInterestFinder;
        set => _playerInterestFinder = value;
    }

    //To check if the player is in the plank placement area
    public bool PlayerInTrigger
    {
        get => _playerInTrigger;
        set => _playerInTrigger = value;
    }
}
