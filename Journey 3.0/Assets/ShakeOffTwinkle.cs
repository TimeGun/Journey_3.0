using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOffTwinkle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FlyTwinkleController.instance.ShakeOffTwinkle();
        }
    }
}
