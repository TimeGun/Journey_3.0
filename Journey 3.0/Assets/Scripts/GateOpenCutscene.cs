using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpenCutscene : MonoBehaviour
{
    private bool gateOpen;
    public GameObject timeline;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        gateOpen = PlayerManager.GateOpened;
        Debug.Log(gateOpen);
        if (gateOpen)
        {
            timeline.GetComponent<TimelineController>().PlayTimeline();
            PlayerManager.GateOpened = false;
            gateOpen = false;
        }
    }
}
