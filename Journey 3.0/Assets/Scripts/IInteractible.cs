using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public interface IInteractible
{
    GameObject getGameObject();

    void StartInteraction(Transform parent);
    

    void StopInteraction();
}
