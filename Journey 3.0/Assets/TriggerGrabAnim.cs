using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGrabAnim : MonoBehaviour
{
    private Coroutine _coroutine;

    public float grabTime;

    private Animator _animator;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            if (_coroutine == null)
            {
                _animator = other.GetComponent<Animator>();
                _coroutine = StartCoroutine(PlayGrabAnim());
            }
        }
    }

    IEnumerator PlayGrabAnim()
    {
        yield return new WaitForSeconds(grabTime);
        _animator.SetTrigger("grab");
    }
}
