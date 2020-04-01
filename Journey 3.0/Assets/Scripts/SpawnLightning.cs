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

    [SerializeField] private bool withinZone;

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
            _player.GetComponent<MountainSlopeSpeed>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            withinZone = false;
            _player.GetComponent<MountainSlopeSpeed>().enabled = false;
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

                Vector3 spawnPos = _player.transform.position;


                Vector3 playerVelocity = _player.GetComponent<CharacterController>().velocity;
                

                Vector3 to = (spawnPos + transform.forward) - spawnPos;

                Vector3 from = (spawnPos + playerVelocity) - spawnPos;

                float angle = Vector3.Angle(to, from);
                
                
                float velocityMultiplier = Map(angle, 180f, 0, 0, 1);
                
                spawnPos += playerVelocity * velocityMultiplier;
                
                
                Instantiate(_lightningPrefab, spawnPos, transform.rotation);
            }
            
            yield return new WaitForEndOfFrame();
        }
    }
    
    public float Map(float a, float b, float c, float d, float e)
    {
        
        float cb = c - b;
        float de = e - d;
        float howFar = (a - b) / cb;
        return d + howFar * de;
        
        //float a = value you want mapped t
    }

}
