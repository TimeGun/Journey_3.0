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
    public int targetPriority;
    public GameObject targetCamera;
    private CinemachineVirtualCamera vcam;
    private double _timelineLength;
    private bool cinematicStarted;
    public float playerFreezeTime;
    public bool freezePlayerForEntireCinematic;
    public bool blackBarsEnabled;

    public float timeBeforeFreeze;
    // Start is called before the first frame update
    void Start()
    {
        _timelineLength = playableDirector.duration;
        vcam = targetCamera.GetComponent<CinemachineVirtualCamera>();
        _startPriority = vcam.Priority;
        if (freezePlayerForEntireCinematic)
        {
            playerFreezeTime = (float) _timelineLength;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cameraTrigger != null)
        {
            _playerEntered = cameraTrigger.GetComponent<DetectPlayer>().PlayerEntered;    //Use a coroutine to check if the player has entered every x amount of frames

        }
//        Debug.Log(_playerEntered);
        if (_playerEntered && cinematicStarted == false)
        {
            StartCoroutine(StartTimeline());
            Debug.Log("Here");
            cinematicStarted = true;


        }
    }


    IEnumerator StartTimeline()
    {
        
        
       
        
        vcam.Priority = targetPriority;
        playableDirector.Play();
        CinematicPlayerMoment.instance.FreezePlayer(timeBeforeFreeze, playerFreezeTime, blackBarsEnabled);
        yield return new WaitForSeconds((float)_timelineLength);
        vcam.Priority = _startPriority;
    }
}
