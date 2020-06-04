using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicObjectToLookAt : MonoBehaviour
{
    public void OnEnable()
    {
        InterestFinder.instance.AddObject(transform, true);
    }

    private void OnDestroy()
    {
        InterestFinder.instance.RemoveObject(transform, true);
    }

    private void OnDisable()
    {
        InterestFinder.instance.RemoveObject(transform, true);
    }
}
