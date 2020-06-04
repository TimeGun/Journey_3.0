using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToLookAt : MonoBehaviour
{
    public float range = 15f;
    public LookAtObject lookAtObject;

    public bool debug = false;
    
    public void OnEnable()
    {
        lookAtObject = new LookAtObject(transform, range);
        InterestFinder.instance.AddObject(lookAtObject, false);
    }

    private void OnDestroy()
    {
        InterestFinder.instance.RemoveObject(lookAtObject, false);
    }

    private void OnDisable()
    {
        InterestFinder.instance.RemoveObject(lookAtObject, false);
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = new Color(50f/255f,205f/255f,50f/255f, 0.05f);
            Gizmos.DrawSphere(transform.position, range);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(50f/255f,205f/255f,50f/255f, 0.3f);
        Gizmos.DrawSphere(transform.position, range);
    }
}
