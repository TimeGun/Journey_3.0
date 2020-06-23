using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayerOnBridge : MonoBehaviour
{
    [SerializeField] private float oldSpeed = 3.5f;
    [SerializeField] private float bridgeSpeed = 2f;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            other.GetComponent<PlayerMovement>().Speed = bridgeSpeed;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            other.GetComponent<PlayerMovement>().Speed = oldSpeed;
        }
    }
}
