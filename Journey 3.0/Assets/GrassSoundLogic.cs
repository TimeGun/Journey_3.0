using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSoundLogic : MonoBehaviour
{
    public Transform sourceObject;

    private Transform player;

    public AudioSource audioSource;

    private CharacterController cc;

    private bool within = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.isTrigger == false)
        {
            player = other.transform;
            cc = player.GetComponent<CharacterController>();
            within = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.isTrigger == false)
        {
            within = false;
        }
    }

    private void Update()
    {
        if (within)
        {
            if (cc.velocity.magnitude > 0.5f)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                sourceObject.position = player.position;
            }
            else
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Pause();
                }
            }
        }
    }
}
