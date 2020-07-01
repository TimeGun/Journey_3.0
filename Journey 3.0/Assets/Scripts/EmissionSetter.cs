using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionSetter : MonoBehaviour
{
    private bool within;
    
    private Color startColor;
    private Color brightColor;
    
    public Material[] _materials;

    private float multiplier;


    private void Start()
    {
        startColor = _materials[0].color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            within = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            within = false;
        }
    }


    private void Update()
    {
        brightColor = startColor * multiplier;

        for (int i = 0; i < 2; i++)
        {
            _materials[i].SetColor("_EmissiveColor", brightColor);
        }
        
        
        if (within)
        {
            multiplier = Mathf.Lerp(multiplier, 4f, Time.deltaTime * 4f);
        }
        else
        {
            multiplier = Mathf.Lerp(multiplier, 0.5f, Time.deltaTime * 4f);
        }
    }
}
