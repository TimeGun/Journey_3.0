using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible
{
    void StartInteraction(Transform parent);
    

    void StopInteraction();
}
