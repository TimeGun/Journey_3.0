using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    [SerializeField] private float _elevatorMovementSpeed;

    public float hardStopValue = -215.3f;

    public PlayAudioOnTrigger _elevatorCrash;

    public float ElevatorMovementSpeed
    {
        get => _elevatorMovementSpeed;
        set => _elevatorMovementSpeed = value;
    }
    
    void Update()
    {
        if (transform.position.y > hardStopValue)
        {
            transform.Translate(-Vector3.up * _elevatorMovementSpeed * Time.deltaTime);
        }
        else
        {
            if (!_elevatorCrash.played)
            {
                _elevatorCrash.PlaySoundOnce();
            }
        }
    }
}
