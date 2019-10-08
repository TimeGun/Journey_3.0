using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRune : MonoBehaviour
{
    [SerializeField] private float radius;


    [SerializeField] private SphereCollider col;

    [SerializeField] private List<GameObject> collidingObjects = new List<GameObject>();
    void Awake()
    {
    }
    
    

    void Update()
    {
        col.radius = radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!collidingObjects.Contains(other.gameObject))
        {
            collidingObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        collidingObjects.Remove(other.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawSphere(transform.position, radius);
    }
}
