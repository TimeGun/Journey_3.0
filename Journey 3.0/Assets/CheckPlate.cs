using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckPlate : MonoBehaviour
{
    [SerializeField] private bool _boulderPresent;

    private bool opened = false;

    [SerializeField] private UnityEvent openDoor;
    
    private void OnTriggerEnter(Collider other)
    {
        PushObject _pushable = other.GetComponent<PushObject>();

        if (_pushable != null)
        {
            _boulderPresent = true;
            if (!opened)
            {
                openDoor.Invoke();
                opened = true;
            }
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
