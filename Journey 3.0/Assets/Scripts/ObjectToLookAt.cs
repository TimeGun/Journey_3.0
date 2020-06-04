using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToLookAt : MonoBehaviour
{
    public void OnEnable()
    {
        InterestFinder.instance.AddObject(transform, false);
    }

    private void OnDestroy()
    {
        InterestFinder.instance.RemoveObject(transform, false);
    }

    private void OnDisable()
    {
        InterestFinder.instance.RemoveObject(transform, false);
    }
}
