using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class PlayOpeningCinematic : MonoBehaviour
{
    
    public PlayableDirector playableDirector;
    public static double _timelineLength;
    private bool played;

    public AudioSource audioSource;
    
    public static PlayOpeningCinematic instance;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _timelineLength = instance.playableDirector.duration;
    }
    
    public void CheckCinematic()
    {
        if (played == false)
        {
            StartCoroutine(StartTimeline());
            Invoke("StartAudio", 2.5f);
        }
    }

    void StartAudio()
    {
        audioSource.Play();
    }

    public static IEnumerator StartTimeline()
    {
        LevelSelectEnabler.DisableButton();
        instance.playableDirector.Play();
        CinematicPlayerMoment.instance.FreezePlayer(0f, (float)_timelineLength, true);
        yield return new WaitForSeconds((float)_timelineLength);
        LevelSelectEnabler.EnableButton();
        instance.played = true;

    }
}
