using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLightning : MonoBehaviour
{
    private GameObject _player;

    [SerializeField] private GameObject _lightningPrefab;
    
    [SerializeField] private float _timeBetweenStrike;

    private Coroutine coroutine;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coroutine = StartCoroutine(SpawnLightningPrefab());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(coroutine);
    }

    public void StartLightningBattle()
    {
        StartCoroutine(SpawnLightningPrefab());
    }

    IEnumerator SpawnLightningPrefab()
    {
        while(true)
        {
            yield return new WaitForSeconds(_timeBetweenStrike);

            Instantiate(_lightningPrefab, _player.transform.position, transform.rotation);
        }
    }
}
