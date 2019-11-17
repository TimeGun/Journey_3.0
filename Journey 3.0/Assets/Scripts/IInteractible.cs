using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public interface IInteractible
{
    
    void StartInteraction(Transform parent);
    

    void StopInteraction();
}
