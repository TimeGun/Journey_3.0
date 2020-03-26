using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ScaleFlame : MonoBehaviour
{
    public VisualEffect _effect;

    public Transform parent;
    
    void Update()
    {
        float flameScale = Map(parent.localScale.x, 1.5f, 3f, 1f, 2.5f);
        _effect.SetFloat("SizeMultiplier 1", flameScale);
    }
    
    public float Map(float a, float b, float c, float d, float e)
    {
        
        float cb = c - b;
        float de = e - d;
        float howFar = (a - b) / cb;
        return d + howFar * de;
        
        //float a = value you want mapped t
    }
}
