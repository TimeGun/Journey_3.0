using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class PlayTimeline : MonoBehaviour
{
    public GameObject cameraTrigger;
    public GameObject targetCamera;

    private DetectPlayer _detectPlayer;
    
    public PlayableDirector playableDirector;
    
    private CinemachineVirtualCamera vcam;

    private int _startPriority;
    public int targetPriority;
    
    private double _timelineLength;
    
    private bool cinematicStarted;
    public bool freezePlayerForEntireCinematic;
    public bool blackBarsEnabled;
    
    public float playerFreezeTime;
    public float timeBeforeFreeze;

    void Start()
    {
        _timelineLength = playableDirector.duration;
        vcam = targetCamera.GetComponent<CinemachineVirtualCamera>();
        _startPriority = vcam.Priority;

        if (cameraTrigger != null)
        {
            _detectPlayer = GetComponent<DetectPlayer>();
        }

        
        if (freezePlayerForEntireCinematic)
        {
            playerFreezeTime = (float) _timelineLength;
        }
    }


    void FixedUpdate()
    {
        if (_detectPlayer != null && _detectPlayer.PlayerEntered && cinematicStarted == false)
        {
            StartCoroutine(StartTimeline());
            cinematicStarted = true;
        }
    }


    IEnumerator StartTimeline()
    {
        LevelSelectEnabler.DisableButton();
        vcam.Priority = targetPriority;
        playableDirector.Play();
        CinematicPlayerMoment.instance.FreezePlayer(timeBeforeFreeze, playerFreezeTime, blackBarsEnabled);
        yield return new WaitForSeconds((float) _timelineLength);
        vcam.Priority = _startPriority;
        LevelSelectEnabler.EnableButton();
    }
}