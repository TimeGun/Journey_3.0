using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTorchS2 : MonoBehaviour
{
    public void DropObject()
    {
        API.GlobalReferences.PlayerRef.GetComponent<InteractWithObject>().StopInteracting();
    }
}
