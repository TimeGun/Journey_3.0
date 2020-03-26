using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetElevatorCogSpeed : MonoBehaviour
{
    [SerializeField] private ElevatorMovement _elevatorMovement;

    [SerializeField] private Animator _animator;

    [SerializeField] private float lastSpeed = 0;
    
   
    // Update is called once per frame
    void Update()
    {
        if (_elevatorMovement.ElevatorMovementSpeed != lastSpeed)
        {
            SetNewSpeed(_elevatorMovement.ElevatorMovementSpeed);
            lastSpeed = _elevatorMovement.ElevatorMovementSpeed;
        }
    }

    void SetNewSpeed(float newSpeed)
    {
        _animator.SetFloat("SpinningSpeed", newSpeed);
    }
}
