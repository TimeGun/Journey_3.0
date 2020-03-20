using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenGrowthRune : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = API.GlobalReferences.PlayerRef;
    }


    private void OnTriggerEnter(Collider other)
    {
        ChangeSize changeSize = other.GetComponent<ChangeSize>();

        if (changeSize != null)
        {
            if (player == null)
            {
                player = GameObject.Find("Player");
            }

            ObjectDetection objectDetection = player.GetComponent<ObjectDetection>();

            InteractWithObject interactWithObject = player.GetComponent<InteractWithObject>();

            if (objectDetection.carryingObject == other.gameObject && changeSize.changeMode)
            {
                interactWithObject.StopInteracting();
            }


            if (changeSize.Small)
            {
                changeSize.StartChangeSize();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("called");
        
        ChangeSize changeSize = other.GetComponent<ChangeSize>();

        if (changeSize != null)
        {
            if (player == null)
            {
                player = GameObject.Find("Player");
            }

            PushObject pushObject = other.GetComponent<PushObject>();

            if (pushObject != null && changeSize.changeMode)
            {
                pushObject.StopInteraction();
            }


            if (!changeSize.Small)
            {
                changeSize.StartChangeSize();
            }
        }
    }
}