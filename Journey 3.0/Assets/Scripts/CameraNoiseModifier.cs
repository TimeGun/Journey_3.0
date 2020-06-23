using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine.Playables;
using UnityEngine.Events;
using UnityEngine.Rendering.UI;

public class CameraNoiseModifier : MonoBehaviour
{
    
    [Tooltip("Amplitude, Frequency, strongShakeLength, lightShakeLength")]public GameObject TargetCamera;
    private CinemachineVirtualCamera vcam;
    private float noiseAmp;
    private float noiseFreq;

    private float shakeLerpSpeed = 10;
    
    private bool testBool;
    private bool coroutineStarted;
    

    public void CameraShake(string newFloatString)
    {
        if (coroutineStarted == false)
        {
            StopAllCoroutines();
            ArrayList noiseArrayList = new ArrayList();
            noiseArrayList = ParseSomething.ParseFloats(newFloatString);
            StartCoroutine(CameraShakeCoroutine( (float)noiseArrayList[0] , (float)noiseArrayList[1], (float)noiseArrayList[2], (float)noiseArrayList[3]));
            
        }
        
    }

    
    
    public IEnumerator CameraShakeCoroutine(float ampChange, float freqChange, float strongShakeLength, float lightShakeLength)
    {
        vcam = TargetCamera.GetComponent<CinemachineVirtualCamera>();
        CinemachineBasicMultiChannelPerlin shaker = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        shaker.m_AmplitudeGain = ampChange;
        shaker.m_FrequencyGain = freqChange;
        
        
        yield return new WaitForSeconds(strongShakeLength);
        float timer = lightShakeLength * 30;
        
        while (timer > 0)
        {
            shaker.m_AmplitudeGain = Mathf.Lerp(
                shaker.m_AmplitudeGain, 0, shakeLerpSpeed/100);
            shaker.m_FrequencyGain = Mathf.Lerp(
                shaker.m_FrequencyGain, 0, shakeLerpSpeed/100);
            timer--;
            
            yield return new WaitForSeconds(Time.deltaTime);
            
        }
        shaker.m_AmplitudeGain = 0;
        shaker.m_FrequencyGain = 0;
        coroutineStarted = false;
        
    }
    
    
    
    
}

public static class ParseSomething
{
        
    public static ArrayList ParseFloats(string floats)
    {
        var strings = floats.Split(',');
        ArrayList floatArray = new ArrayList();
            
        for (var i = 0; i < strings.Length; i++)
        {
//                Debug.Log(i);
            float tempfloat = float.Parse(strings[i]);
            floatArray.Add(tempfloat);
        }
        return floatArray;
    }
}
