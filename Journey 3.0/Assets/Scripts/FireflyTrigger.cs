using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;





public class FireflyTrigger : MonoBehaviour
{
    
    [SerializeField] private float _fleeSpeed;
    public GameObject Camera;
    [SerializeField] private Transform _flies;
    [SerializeField] private ParticleSystem _system;
    private ParticleSystem.EmissionModule _em;
    private ParticleSystem.MainModule _mm;
    private ParticleSystem.VelocityOverLifetimeModule _vol;

    
    
    private bool _flee;

    [SerializeField] private float _timeToFleeFor;
    
    private float _timeSinceFlee;

    [SerializeField] public int twinkleAmmountToAdd = 10;

    private void Start()
    {
        //_system.Stop();        //Gary Added
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _system.Play();    //Gary Added 
        StartCoroutine(Flee());
    }

    IEnumerator Flee()
    {
        FlyTwinkleController.instance.AddTwinkleAmount(twinkleAmmountToAdd);
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
        
        /*_vol = _system.velocityOverLifetime;
        _vol.x = new ParticleSystem.MinMaxCurve(-5,5);
        _vol.y = new ParticleSystem.MinMaxCurve(8,12);
        _vol.z = new ParticleSystem.MinMaxCurve(-5,5);*/

        yield return new WaitForSeconds(_mm.startLifetime.Evaluate(1));
        _system.Stop();
        
        
        Destroy(gameObject);
    }
}
