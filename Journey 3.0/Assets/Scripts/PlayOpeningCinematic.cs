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
    
    public static PlayOpeningCinematic instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _timelineLength = instance.playableDirector.duration;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckCinematic()
    {
        if (played == false)
        {
            StartCoroutine(StartTimeline());
        }
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
