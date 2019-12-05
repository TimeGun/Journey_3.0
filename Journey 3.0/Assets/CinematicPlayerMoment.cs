using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicPlayerMoment : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    public static CinematicPlayerMoment instance;
    
    void Start()
    {
        instance = this;
    }

    public void FreezePlayer(float timeToFreezeFor, bool blackBarsEnabled)
    {
        
    }


    IEnumerator FreezePlayerCoroutine(float time, bool blackBars)
    {
        _playerMovement.enabled = false;

        if (blackBars)
        {
            BlackBarsScript.instance.SetBars(true);
        }

        yield return new WaitForSeconds(time);

        if (blackBars)
        {
            BlackBarsScript.instance.SetBars(false);
        }

        _playerMovement.enabled = true;
    }
}
