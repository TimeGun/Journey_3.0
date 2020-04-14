using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightChanger : MonoBehaviour
{

    [SerializeField] private float timeToLerpFor;

    public void LocalLerpDayToNight(float timeToLerpFor)
    {
        LerpDayToNight.LerpToNight(timeToLerpFor);
    }
}
