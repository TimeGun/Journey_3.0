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
        gateOpen = PlayerManager.GateOpened;
    }

    // Update is called once per frame
    void Update()
    {
        if (gateOpen)
        {
            timeline.GetComponent<TimelineController>().playTimeline = true;
        }
    }
}
