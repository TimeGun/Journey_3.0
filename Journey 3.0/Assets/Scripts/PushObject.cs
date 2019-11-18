using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class PushObject : MonoBehaviour, IInteractible
{
    private GameObject player;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void StartInteraction(Transform parent)
    {
        player = parent.root.gameObject;
        player.GetComponent<PlayerMovement>().Pushing = true;
    }

    public void StopInteraction()
    {
        player.GetComponent<PlayerMovement>().Pushing = false;
    }
}
