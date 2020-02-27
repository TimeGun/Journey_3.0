using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTextPopup : MonoBehaviour
{
    [SerializeField] private string textToDisplay;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            TextPopup.instance.StartCoroutine(TextPopup.instance.DisplayText(textToDisplay));
        }
    }
}
