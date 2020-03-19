using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainSlopeSpeed : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;

    [SerializeField] private PlayerMovement _playerMovement;

    private float maxWalkSpeedStart;

    [SerializeField] private float minimumMaxSpeed = 0.5f;
    
    
    
    void OnEnable()
    {
        maxWalkSpeedStart = _playerMovement.Speed;
    }
    
    void Update()
    {
        Vector3 to = (transform.position + new Vector3(_characterController.velocity.x, 0, _characterController.velocity.z)) - transform.position;

        Vector3 from = (transform.position + _characterController.velocity) - transform.position;
        
        
        float slopeAngle = Vector3.Angle(to, from);

        if (from.y > to.y)
        {
            _playerMovement.Speed = _playerMovement.Map(slopeAngle, _characterController.slopeLimit, 0f, minimumMaxSpeed, maxWalkSpeedStart);
        }
        else
        {
            _playerMovement.Speed = maxWalkSpeedStart;
        }

    }

    private void OnDisable()
    {
        _playerMovement.Speed = maxWalkSpeedStart;
    }
}
