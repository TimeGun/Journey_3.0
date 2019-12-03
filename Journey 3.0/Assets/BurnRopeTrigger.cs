using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BurnRopeTrigger : MonoBehaviour
{

    public UnityEvent burnedRope;
    

    private void OnTriggerEnter(Collider other)
    {
        TorchOnOff torchOnOff = other.GetComponent<TorchOnOff>();

        if (torchOnOff != null)
        {
            print("Oi cunt");

            burnedRope.Invoke();
        }
    }
}
