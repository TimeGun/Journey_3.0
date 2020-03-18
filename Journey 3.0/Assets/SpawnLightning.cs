using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLightning : MonoBehaviour
{
    private GameObject _player;

    [SerializeField] private GameObject _lightningPrefab;
    
    [SerializeField] [Range(0.5f, 10f)] private float _timeBetweenStrike;

    private Coroutine coroutine;

    private bool withinZone;

    private void Start()
    {
        _player = GameObject.Find("Player");

        StartCoroutine(SpawnLightningPrefab());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            withinZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            withinZone = false;
        }
    }

    public void StartLightningBattle()
    {
        StartCoroutine(SpawnLightningPrefab());
    }

    IEnumerator SpawnLightningPrefab()
    {
        while(true)
        {
            if (withinZone)
            {
                yield return new WaitForSeconds(_timeBetweenStrike);

                Instantiate(_lightningPrefab, _player.transform.position, transform.rotation);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
