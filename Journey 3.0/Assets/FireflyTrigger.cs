using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class FireflyTrigger : MonoBehaviour
{
    
    [SerializeField] private float _fleeSpeed;
    
    [SerializeField] private Transform _flies;
    [SerializeField] private ParticleSystem _system;
    private ParticleSystem.EmissionModule _em;
    private ParticleSystem.MainModule _mm;

    private bool _flee;

    [SerializeField] private float _timeToFleeFor;
    
    private float _timeSinceFlee;
    
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Flee());
    }

    IEnumerator Flee()
    {
        _flee = true;
        while (_flee)
        {
            _flies.transform.Translate(Vector3.up * (_fleeSpeed * Time.deltaTime));
            _timeSinceFlee += Time.deltaTime;

            if (_timeSinceFlee > _timeToFleeFor)
            {
                _flee = false;
            }
            yield return new WaitForEndOfFrame();
        }

        _em = _system.emission;
        _mm = _system.main;
        
        _em.rateOverDistance = 0;
        _em.rateOverTime = 0;
        
        yield return new WaitForSeconds(_mm.startLifetime.Evaluate(1));
        Destroy(gameObject);
    }
}
