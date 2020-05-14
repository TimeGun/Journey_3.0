using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeElevatorEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent[] seriesOfEvents;

    [SerializeField] private float[] seriesOfEverntsTimers;
    
    private bool called = false;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !called && !other.isTrigger)
        {
            print("this getting called?");
            StartCoroutine(ExecuteEvents());
            called = true;
        }
    }

    private IEnumerator ExecuteEvents()
    {
        for (int i = 0; i < seriesOfEvents.Length; i++)
        {
            yield return new WaitForSeconds(seriesOfEverntsTimers[i]);
            seriesOfEvents[i].Invoke();
        }
    }
    
}
