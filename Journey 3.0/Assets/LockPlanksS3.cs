using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPlanksS3 : MonoBehaviour
{
    private BridgeSideTrigger one;
    private BridgeSideTrigger two;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (one.Plank != null && two.Plank != null)
        {
            one.Plank.tag = "Locked";
            two.Plank.tag = "Locked";
        }
    }
}
