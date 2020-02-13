using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpenCutscene : MonoBehaviour
{
    private bool gateOpen;
    public GameObject timeline;

    public TimelineController _timelineController;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        gateOpen = PlayerManager.GateOpened;
        
        if (gateOpen)
        {
            timeline.GetComponent<TimelineController>().PlayTimeline();
            _timelineController.StartCoroutine(_timelineController.PriorityChange());
            PlayerManager.GateOpened = false;
            gateOpen = false;
        }
    }
}
