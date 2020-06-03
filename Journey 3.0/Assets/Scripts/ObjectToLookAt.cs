using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToLookAt : MonoBehaviour
{
    public void OnEnable()
    {
        InterestFinder.instance.AddObject(transform);
    }

    private void OnDestroy()
    {
        InterestFinder.instance.RemoveObject(transform);
    }

    private void OnDisable()
    {
        InterestFinder.instance.RemoveObject(transform);
    }
}
