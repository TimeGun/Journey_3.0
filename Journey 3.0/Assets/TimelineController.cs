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
    public bool ropeCutScene;
    public bool playTimeline;
    public bool rockFall;
    
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
        
        
        if (playTimeline)
        {
            //playableDirector.Play();
            StartCoroutine(JankyRope());
            //playableDirector.Pause();
            playTimeline = false;
        }
        
        if (ropeCutScene)
        {
            StartCoroutine(JankyRope());
            ropeCutScene = false;
            //playableDirector.Stop();
            //playableDirector.Evaluate();
        }

        if (cameraTrigger != null)
        {
            _playerEntered = cameraTrigger.GetComponent<DetectPlayer>().PlayerEntered;    //Use a coroutine to check if the player has entered every x amount of frames

        }
        
        if (_playerEntered && cinematicStarted == false)
        {
            //Debug.Log("yurt");
            if (targetCamera != null)
            {
                StartCoroutine(PriorityChange());
            }

            if (rockFall)
            {
                PlayTimeline();
                rockFall = false;
            }
           
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

    void PlayTimeline()
    {
        playableDirector.Play();
    }

    IEnumerator JankyRope()
    {
        yield return new WaitForSeconds((float)_timelineLength);
        yield return new WaitForSeconds(3.4f);
        playableDirector.Play();
        yield return new WaitForSeconds(0.1f);
        playableDirector.Pause();
        //playableDirector.time = 0.3f;
    }
}
