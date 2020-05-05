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
    //public int strongShakeTime;
    //public int lightShakeTime;
    private float shakeLerpSpeed = 10;
    

    //public float testAmp;
    //public float testFreq;

    private bool testBool;

    private bool coroutineStarted;
    
    // Start is called before the first frame update
    void Start()
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if (testBool)
        {
            testBool = false;
            //CameraShake(testAmp, testFreq, strongShakeTime, lightShakeTime);
            //StartCoroutine(CameraShakeCoroutine(testAmp, testFreq, strongShakeTime, lightShakeTime));
           
        }
    }

    public void CameraShake(string newFloatString)
    {
        if (coroutineStarted == false)
        {
            ArrayList noiseArrayList = new ArrayList();
            noiseArrayList = ParseSomething.ParseFloats(newFloatString);
            StartCoroutine(CameraShakeCoroutine( (float)noiseArrayList[0] , (float)noiseArrayList[1], (float)noiseArrayList[2], (float)noiseArrayList[3]));
            
        }
        
    }

    
    
    public IEnumerator CameraShakeCoroutine(float ampChange, float freqChange, float strongShakeLength, float lightShakeLength)
    {
        //coroutineStarted = true;
//        Debug.Log(ampChange);
        vcam = TargetCamera.GetComponent<CinemachineVirtualCamera>();
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = ampChange;
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = freqChange;
        //noiseAmp = ampChange;
        //noiseFreq = freqChange;
        yield return new WaitForSeconds(strongShakeLength);
        float timer = lightShakeLength * 30;
        while (timer > 0)
        {
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Mathf.Lerp(
                vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain, 0, shakeLerpSpeed/100);
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = Mathf.Lerp(
                vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain, 0, shakeLerpSpeed/100);
            timer--;
            yield return new WaitForSeconds(Time.deltaTime);
            
        }
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
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
