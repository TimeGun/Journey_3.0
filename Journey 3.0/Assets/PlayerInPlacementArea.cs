using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInPlacementArea : MonoBehaviour
{
    [SerializeField] private bool _playerInTrigger;

    public bool PlayerInTrigger
    {
        get => _playerInTrigger;
        set => _playerInTrigger = value;
    }
}
