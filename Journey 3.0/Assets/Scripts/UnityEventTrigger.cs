using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventTrigger : MonoBehaviour
{
    [SerializeField] public UnityEvent _unityEventToTrigger;

    [SerializeField] private bool _destroySelf = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            print("Event got triggered");
            _unityEventToTrigger.Invoke();

            if (_destroySelf)
            {
                Destroy(gameObject);
            }
        }
    }
}
