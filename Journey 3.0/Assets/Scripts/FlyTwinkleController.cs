using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FlyTwinkleController : MonoBehaviour
{
    [SerializeField] private VisualEffect _twinkleVFX;

    public static FlyTwinkleController instance;

    private void Start()
    {
        instance = this;
    }

    public void ShakeOffTwinkle()
    {
        _twinkleVFX.SetBool("ShakeOff", !_twinkleVFX.GetBool("ShakeOff"));
        _twinkleVFX.SetFloat("TwinkleCount", 0);
    }

    void EnablePickup()
    {
        
       _twinkleVFX.SetBool("ShakeOff", false);
    }

    public void AddTwinkleAmount(int ammountToAdd)
    {
        _twinkleVFX.SetFloat("TwinkleCount", _twinkleVFX.GetFloat("TwinkleCount") + ammountToAdd);
    }
}
