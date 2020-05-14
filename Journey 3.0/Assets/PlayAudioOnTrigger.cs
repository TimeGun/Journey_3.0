using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnTrigger : MonoBehaviour
{
    [SerializeField]private AudioSource _source;

    private bool played = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && played == false)
        {
            print("Playing Audio");
            _source.Play();
            played = true;
        }
    }
}
