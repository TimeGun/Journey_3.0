using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPlanksS3 : MonoBehaviour
{
    [SerializeField] private BridgeSideTrigger one;
    [SerializeField] private BridgeSideTrigger two;
    

    void Update()
    {
        if (one.Plank != null && two.Plank != null)
        {
            one.Plank.tag = "Locked";
            two.Plank.tag = "Locked";
            this.enabled = false;
        }
    }
}