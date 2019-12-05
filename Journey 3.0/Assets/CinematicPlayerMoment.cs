using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicPlayerMoment : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    public static CinematicPlayerMoment instance;

    private bool running;

    void Start()
    {
        instance = this;
        
        FreezePlayer(3,5, true);
    }

    public void FreezePlayer(float startFreezeIn, float timeToFreezeFor, bool blackBarsEnabled)
    {
        if (!running)
            StartCoroutine(FreezePlayerCoroutine(startFreezeIn, timeToFreezeFor, blackBarsEnabled));
    }


    IEnumerator FreezePlayerCoroutine(float startIn, float time, bool blackBars)
    {
        running = true;

        yield return new WaitForSeconds(startIn);
        _playerMovement.DisableThis();

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
        running = false;
    }
}