using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTorchS2 : MonoBehaviour
{
    public void DropObject()
    {
        Invoke("DropTorch", 0.5f);
    }

    void DropTorch()
    {
        API.GlobalReferences.PlayerRef.GetComponent<InteractWithObject>().StopInteracting();
    }
}
