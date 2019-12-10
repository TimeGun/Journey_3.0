using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAudioPlay : MonoBehaviour
{
    public AudioSource _source;
    public AudioClip _clip;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 8)
        {
            _source.PlayOneShot(_clip);
            Destroy(this);
        }
    }

    
}
