using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractibleObject: MonoBehaviour
{
    private bool _grabbed;
    
    virtual public void StartInteraction(Transform parent){}

    virtual public void StopInteraction() {}
}
