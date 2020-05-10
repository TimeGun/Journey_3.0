using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokingStart : MonoBehaviour
{
    public float smokeTime = 10f;
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FlowerSmokeController.instance.StartSmoking(smokeTime);
        }
    }
}
