using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAdjustment : MonoBehaviour
{
    [SerializeField] private PlayerInPlacementArea _playerInPlacementArea;

    [SerializeField] private bool plankPlacedDown = false;

    private GameObject _playerObject;
    
    void Start()
    {
        _playerObject = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        //if the player is in the placement area and a plank hasnt been placed yet
        if (_playerInPlacementArea.PlayerInTrigger && !plankPlacedDown)
        {
            AdjustPosition();
        }
    }

    private void AdjustPosition()
    {
        Vector3 _playerPos = _playerObject.transform.position;

        _playerPos.y = 0;
        
    }
}
