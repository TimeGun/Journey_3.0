using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BrokenGrowthRune : MonoBehaviour
{
    private GameObject player;

    [SerializeField] private VisualEffect _slugs;

    void Start()
    {
        player = API.GlobalReferences.PlayerRef;
    }


    private void OnTriggerStay(Collider other)
    {
        ChangeSize changeSize = other.GetComponent<ChangeSize>();

        if (changeSize != null && changeSize.Small)
        {
            _slugs.SetBool("BoulderInRange", true);
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
        
        ChangeSize changeSize = other.GetComponent<ChangeSize>();

        if (changeSize != null)
        {
            _slugs.SetBool("BoulderInRange", false);

            if (player == null)
            {
                player = GameObject.Find("Player");
            }

            PushObject pushObject = other.GetComponent<PushObject>();

            if (pushObject != null && changeSize.changeMode && pushObject.isActive())
            {
                player.GetComponent<InteractWithObject>().StopInteracting();
            }


            if (!changeSize.Small)
            {
                changeSize.StartChangeSize();
            }
        }
    }
}