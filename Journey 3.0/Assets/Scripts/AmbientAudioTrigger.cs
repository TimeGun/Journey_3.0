using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AmbientAudioTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _event.Invoke();
            Destroy(gameObject);
        }
        
    }

    public void SetOutSideAmbience(float newVolume)
    {
        AmbienceManager.instance.SetOutSideAmbience(newVolume);
    }

    public void SetInsideAmbience(float newVolume)
    {
        AmbienceManager.instance.SetInsideAmbience(newVolume);

    }

    public void SetMountainTopAmbience(float newVolume)
    {
        AmbienceManager.instance.SetTopOfTheMountainAmbience(newVolume);

    }
}
