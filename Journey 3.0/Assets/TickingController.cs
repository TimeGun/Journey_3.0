using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TickingController : MonoBehaviour
{

    public AudioSource _source;

    [SerializeField] private AudioClip _normal;
    [SerializeField] private AudioClip _impact;


    private int tickCounter;


    public void TickSound()
    {
        if (tickCounter % 3 == 0)
        {
            _source.PlayOneShot(_impact);
        }
        else
        {
            _source.PlayOneShot(_normal);
        }
        
        tickCounter++;
    }
}
