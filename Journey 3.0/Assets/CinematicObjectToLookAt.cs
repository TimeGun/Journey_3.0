using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicObjectToLookAt : MonoBehaviour
{
    public float range = 15f;
    public LookAtObject lookAtObject;

    public bool debug = false;
    public void OnEnable()
    {
        lookAtObject = new LookAtObject(transform, range);
        InterestFinder.instance.AddObject(lookAtObject, true);
    }

    private void OnDestroy()
    {
        InterestFinder.instance.RemoveObject(lookAtObject, true);
    }

    private void OnDisable()
    {
        InterestFinder.instance.RemoveObject(lookAtObject, true);
    }
    
    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = new Color(175f/255f,238f/255f,238f/255f, 0.05f);
            Gizmos.DrawSphere(transform.position, range);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(175f/255f,238f/255f,238f/255f, 0.3f);
        Gizmos.DrawSphere(transform.position, range);
    }
}
