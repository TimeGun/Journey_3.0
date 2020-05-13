using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlankS6 : MonoBehaviour
{
    [SerializeField] private PlankPlacement _plankPlacement;



    private void Update()
    {
        if (_plankPlacement.GetPlank() != null)
        {
            _plankPlacement.GetPlank().tag = "Locked";
        }
    }
}
