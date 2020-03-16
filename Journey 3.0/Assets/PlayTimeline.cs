using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class PlayTimeline : MonoBehaviour
{
    public GameObject cameraTrigger;
    private bool _playerEntered;
    public PlayableDirector playableDirector;
    private int _startPriority;
//    public int targetPriority;
//    public GameObject targetCamera;
    private CinemachineVirtualCamera vcam;
    private double _timelineLength;
    private bool cinematicStarted;
//    public bool playTimeline;

    public float timeBeforeFreeze;
    // Start is called before the first frame update
    void Start()
    {
        _timelineLength = playableDirector.duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraTrigger != null)
        {
            _playerEntered = cameraTrigger.GetComponent<DetectPlayer>().PlayerEntered;    //Use a coroutine to check if the player has entered every x amount of frames

        }
        if (_playerEntered && cinematicStarted == false)
        {
            StartCoroutine(StartTimeline());
            cinematicStarted = true;


        }
    }


    IEnumerator StartTimeline()
    {
        playableDirector.Play();
        //CinematicPlayerMoment.instance.FreezePlayer(timeBeforeFreeze, (float)_timelineLength, true);
        yield return new WaitForSeconds((float)_timelineLength);
    }
}
