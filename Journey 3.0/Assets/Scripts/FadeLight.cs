using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeLight : MonoBehaviour
{
    public InteractWithObject _interactWithObject;
    
    public Material _material;
    public Light _light;

    private Color startColor;
    private Color brightColor;

    private float multiplier;

    private float startIntensity;
    
    private float intensityMultiplier = 1f;

    private void Start()
    {
        startColor = new Color(0, 1f, 1f, 1f);
        startIntensity = _light.intensity;
        _interactWithObject = API.GlobalReferences.PlayerRef.GetComponent<InteractWithObject>();
    }

    void Update()
    {
        brightColor = startColor * multiplier;
        
        _material.SetColor("_EmissiveColor", brightColor);
        _light.intensity = startIntensity * intensityMultiplier;
        
        
        if (_interactWithObject.NearRune)
        {
            multiplier = Mathf.Lerp(multiplier, 4f, Time.deltaTime * 2f);
            intensityMultiplier = Mathf.Lerp(intensityMultiplier, 5f, Time.deltaTime * 2f);
        }
        else
        {
            multiplier = Mathf.Lerp(multiplier, 0.5f, Time.deltaTime * 2f);
            intensityMultiplier = Mathf.Lerp(intensityMultiplier, 1f, Time.deltaTime * 2f);
        }
    }
}
