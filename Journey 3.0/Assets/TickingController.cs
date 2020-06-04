using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class TickingController : MonoBehaviour
{

    public AudioSource _source;

    public VisualEffect _dust;

    [SerializeField] private AudioClip _normal;
    [SerializeField] private AudioClip _impact;


    private int tickCounter;


    public void TickSound()
    {
        if (tickCounter % 3 == 0)
        {
            _source.PlayOneShot(_impact);
            _dust.SendEvent("DustSpawn");
        }
        else
        {
            _source.PlayOneShot(_normal);
        }
        
        tickCounter++;
    }
}
