using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlate : MonoBehaviour
{
    public bool _boulderPresent;
    
    private void OnTriggerEnter(Collider other)
    {
        PushObject _pushable = other.GetComponent<PushObject>();

        if (_pushable != null)
        {
            _boulderPresent = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PushObject _pushable = other.GetComponent<PushObject>();

        if (_pushable != null)
        {
            _boulderPresent = false;
        }
    }
}
