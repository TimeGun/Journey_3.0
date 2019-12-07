using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;



public class TimelineController : MonoBehaviour
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
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(playableDirector.duration);
        vcam = targetCamera.GetComponent<CinemachineVirtualCamera>();
        _startPriority = vcam.Priority;
        _timelineLength = playableDirector.duration;
//        Debug.Log(_startPriority);


    }

    // Update is called once per frame
    void Update()
    {
        _playerEntered = cameraTrigger.GetComponent<DetectPlayer>().PlayerEntered;    //Use a coroutine to check if the player has entered every x amount of frames
        if (_playerEntered && cinematicStarted == false)
        {
            //Debug.Log("yurt");
            StartCoroutine(PriorityChange());
            SetCamera();
            cinematicStarted = true;


        }
    }

    public void SetCamera()
    {
        //playableDirector.Play();
    }

    IEnumerator PriorityChange()
    {
        
        bool runOnce = true;
            while (runOnce)
            {
                //Debug.Log(testInt);
                Debug.Log("Coroutine");
                
                vcam.Priority = targetPriority;
                //Debug.Log(_timelineLength);
                //float tempTimelineLength = (float) _timelineLength;
                playableDirector.Play();
                CinematicPlayerMoment.instance.FreezePlayer(0, (float)_timelineLength, true);
                Debug.Log(_timelineLength);
                yield return new WaitForSeconds((float)_timelineLength);
                Debug.Log("sp = " + _startPriority);
                vcam.Priority = _startPriority;
                
                runOnce = false;
                
            }        
    }
}
