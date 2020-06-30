using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PushingParticles : MonoBehaviour
{

    [SerializeField] private VisualEffect pushDust;
    
    public void SetSmoke(bool moving, bool dir)
    {
        pushDust.SetBool("Spawn", moving);
        pushDust.SetBool("Pulling", dir);
    }
    
}
