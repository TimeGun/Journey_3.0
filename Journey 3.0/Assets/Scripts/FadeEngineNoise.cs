using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEngineNoise : MonoBehaviour
{
    public void FadeNoise()
    {
        FadeOutVolume[] engines = FindObjectsOfType<FadeOutVolume>();

        foreach (var engine in engines)
        {
            engine.FadeOut();
        }
    }
}
