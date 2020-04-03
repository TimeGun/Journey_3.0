using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardPlaceBoulder : MonoBehaviour
{
    [SerializeField] private Transform placeToPutBoulder;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ChangeSize>())
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            other.transform.position = placeToPutBoulder.position;
        }
    }
}
