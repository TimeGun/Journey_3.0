using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectBack : MonoBehaviour
{
    [SerializeField] private Transform resetLocation;
    private void OnTriggerEnter(Collider other)
    {
        IInteractible interactible = other.GetComponent<IInteractible>();

        if (interactible != null)
        {
            interactible.getGameObject().transform.position = resetLocation.position;
        }
    }
}
