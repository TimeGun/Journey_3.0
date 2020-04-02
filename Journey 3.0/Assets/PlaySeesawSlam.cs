using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySeesawSlam : MonoBehaviour
{
    
    [SerializeField] private AudioSource _slamAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SeesawTip"))
        {
            print("slam time");
            _slamAudio.PlayOneShot(_slamAudio.clip);
        }
    }
}
