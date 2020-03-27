using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBoulderPresent : MonoBehaviour
{
    [SerializeField] private bool boulder = false;

    public Transform pushableObj;

    public bool Boulder
    {
        get => boulder;
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PushObject>() != null)
        {
            pushableObj = other.transform;
            boulder = true;
        }

    }
    

    bool BoublerIsPresent()
    {
        return boulder;
    }
}