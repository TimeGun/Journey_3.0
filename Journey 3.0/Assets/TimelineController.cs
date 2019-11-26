using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;



public class TimelineController : MonoBehaviour
{
    public GameObject cameraTrigger;
    private bool _playerEntered;
    public PlayableDirector playableDirector;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerEntered = cameraTrigger.GetComponent<DetectPlayer>().PlayerEntered;
        if (_playerEntered)
        {
            //Debug.Log("yurt");
            SetCamera();
        }
    }

    public void SetCamera()
    {
        playableDirector.Play();
    }
}
