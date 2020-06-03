using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTorchS2 : MonoBehaviour
{
    public void DropObject()
    {
        Invoke("DropTorch", 1f);
    }

    void DropTorch()
    {
        InteractWithObject _interactWithObject = API.GlobalReferences.PlayerRef.GetComponent<InteractWithObject>();
        GameObject.Find("InteractibleTorch - Final").GetComponent<ObjectToLookAt>().enabled = false;
        _interactWithObject.StopInteracting();
    }
}
