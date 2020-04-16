using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomClusterTrigger : MonoBehaviour
{
    [SerializeField] private Animator[] _mushroomAnimators;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ShootSpores();
        }
    }

    void ShootSpores()
    {
        for (int i = 0; i < _mushroomAnimators.Length; i++)
        {
            _mushroomAnimators[i].Play("MovingMushroom");
        }
    }
}
