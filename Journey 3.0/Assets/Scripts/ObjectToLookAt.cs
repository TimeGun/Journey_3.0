using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToLookAt : MonoBehaviour
{
    private void OnDestroy()
    {
        InterestFinder.instance.RemoveObject(transform);
    }

    private void OnDisable()
    {
        InterestFinder.instance.RemoveObject(transform);
    }
}
