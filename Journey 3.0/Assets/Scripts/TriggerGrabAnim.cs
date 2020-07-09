using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGrabAnim : MonoBehaviour
{
    private Coroutine _coroutine;

    public float grabTime;

    private Animator _animatorPlayer;

    public Animator _animatorFlies;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            if (_coroutine == null)
            {
                _animatorPlayer = other.GetComponent<Animator>();
                print(_animatorPlayer);
                _coroutine = StartCoroutine(PlayGrabAnim());
            }
        }
    }

    IEnumerator PlayGrabAnim()
    {
        yield return new WaitForSeconds(grabTime);
        _animatorPlayer.SetFloat("grabSpeedMult", 0.4f);
        _animatorPlayer.SetTrigger("grab");
        _animatorFlies.SetTrigger("Flee");
        FireflyAchievement.AddFirefly();
        
        yield return new WaitForSeconds(2f);
        //_animatorPlayer.GetComponent<InterestFinder>().RemoveObject();
        _animatorPlayer.SetFloat("grabSpeedMult", 1);

    }
}
