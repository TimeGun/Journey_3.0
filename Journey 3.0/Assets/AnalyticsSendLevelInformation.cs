using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsSendLevelInformation : MonoBehaviour
{
    [SerializeField] private bool _startLevel;
    [SerializeField] private bool _endLevel;


    [SerializeField] private string parameterName = "level_index";

    [SerializeField] private int levelStartedValue;
    [SerializeField] private int levelFinishedValue;
    
    public void SendLevelAnalytics(){
        if (_startLevel)
        {
            AnalyticsResult levelStartResult = AnalyticsEvent.Custom("level_start", new Dictionary<string, object>()
            {
                {parameterName, levelStartedValue}
            });
            Debug.Log(levelStartResult);
        }
        if (_endLevel)
        {
            AnalyticsResult levelEndResult = AnalyticsEvent.Custom("level_complete", new Dictionary<string, object>()
            {
                {parameterName, levelFinishedValue}
            });
            Debug.Log(levelEndResult);
        }
    }
}
