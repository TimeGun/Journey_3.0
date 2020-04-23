using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsSendFrameRate : MonoBehaviour
{
    public void SendAvgFrameRate(){
        //Send the event. Also get the result, so we can make sure it sent correctly
        
        AnalyticsResult result = AnalyticsEvent.Custom("average_frameRate", new Dictionary<string, object>
        {
            { "fps_value", AverageFramesPerSecond.GetAverageFrameRate() }
        });
        Debug.Log(result);
    }
}
