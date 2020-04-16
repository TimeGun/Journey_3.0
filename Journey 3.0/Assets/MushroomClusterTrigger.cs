using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MushroomClusterTrigger : MonoBehaviour
{
    [SerializeField] private Animator[] _mushroomAnimators;

    [SerializeField] private VisualEffect _spores;

    private float timer;

    [SerializeField] private float sporeTimer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(timer <= 0)
                ShootSpores();
        }
    }

    private void Update()
    {
        if(timer > 0)
            timer -= Time.deltaTime;
    }

    void ShootSpores()
    {
        for (int i = 0; i < _mushroomAnimators.Length; i++)
        {
            _mushroomAnimators[i].Play("MovingMushroom");
        }
        _spores.SendEvent("Burst");
        timer = sporeTimer;
    }
}
