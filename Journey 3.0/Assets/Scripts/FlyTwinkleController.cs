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
        _twinkleVFX.SetBool("ShakeOff", true);

        Invoke("EnablePickup" , 1.5f);
    }

    void EnablePickup()
    {
        _twinkleVFX.SetFloat("TwinkleCount", 0);
        _twinkleVFX.SetBool("ShakeOff", false);
    }

    public void AddTwinkleAmount(int ammountToAdd)
    {
        _twinkleVFX.SetFloat("TwinkleCount", _twinkleVFX.GetFloat("TwinkleCount") + ammountToAdd);
    }
}
