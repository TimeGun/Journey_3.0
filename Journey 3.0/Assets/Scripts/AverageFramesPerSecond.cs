using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AverageFramesPerSecond : MonoBehaviour
{
    public static float GetAverageFrameRate()
    {
        float avg = Time.frameCount / Time.time;

        return avg;
    }
}