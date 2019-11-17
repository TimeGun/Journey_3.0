using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class FireflyTrigger : MonoBehaviour
{
    
    [SerializeField] private float _fleeSpeed;
    
    [SerializeField] private Transform _flies;

    [SerializeField] private bool flee;
    
    private void OnTriggerEnter(Collider other)
    {
        print("Test");
        StartCoroutine(Flee());
    }

    IEnumerator Flee()
    {
        flee = true;
        while (flee)
        {
            _flies.transform.Translate(Vector3.up * (_fleeSpeed * Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }

    }
}
