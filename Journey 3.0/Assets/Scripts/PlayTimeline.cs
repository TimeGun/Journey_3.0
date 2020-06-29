using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Audio;
using UnityEngine.Playables;

public class PlayTimeline : GradualLoader
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

    public bool useCutsceneAudioSnapshot = false;

    [SerializeField] private AudioMixerSnapshot normalSnapshot;
    [SerializeField] private AudioMixerSnapshot cutsceneSnapshot;
    
    public override void EnqueThis()
    {
        print("Enqued This");
        base.EnqueThis();
    }

    public override void InitialiseThis()
    {
        print("Initialised This");
        base.InitialiseThis();
    }
    
    public override void Awake()
    {
        print("Called Awake");
        base.Awake();
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => initialised);
        
        _timelineLength = playableDirector.duration;
        vcam = targetCamera.GetComponent<CinemachineVirtualCamera>();
        _startPriority = vcam.Priority;

        if (cameraTrigger != null)
        {
            _detectPlayer = cameraTrigger.GetComponent<DetectPlayer>();
        }

        
        if (freezePlayerForEntireCinematic)
        {
            playerFreezeTime = (float) _timelineLength;
        }
    }


    void FixedUpdate()
    {
        if (updateReady)
        {
            Debug.Log(_detectPlayer);
            if (_detectPlayer != null && _detectPlayer.PlayerEntered && cinematicStarted == false)
            {
                StartCoroutine(StartTimeline());
                Debug.Log("Detected Player");
                cinematicStarted = true;
            }
        }
    }


    IEnumerator StartTimeline()
    {
        Debug.Log("In Coroutine");
        LevelSelectEnabler.DisableButton();
        if (useCutsceneAudioSnapshot)
        {
            cutsceneSnapshot.TransitionTo(1f);
        }

        vcam.Priority = targetPriority;
        playableDirector.Play();
        CinematicPlayerMoment.instance.FreezePlayer(timeBeforeFreeze, playerFreezeTime, blackBarsEnabled);
        yield return new WaitForSeconds((float) _timelineLength);
        vcam.Priority = _startPriority;
        if (useCutsceneAudioSnapshot)
        {
            normalSnapshot.TransitionTo(1f);
        }
        LevelSelectEnabler.EnableButton();
    }
}