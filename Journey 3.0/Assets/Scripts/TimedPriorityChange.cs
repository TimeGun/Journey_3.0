using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class TimedPriorityChange : MonoBehaviour
{
    
    public GameObject TargetCamera;
    private CinemachineVirtualCamera vcam;
    private int initialPriority;
    //private int targetPriority;
    //public float secondsToWait;

    // Start is called before the first frame update
    void Start()
    {
        vcam = TargetCamera.GetComponent<CinemachineVirtualCamera>();
        initialPriority  = vcam.Priority;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PriorityChange(string newFloatString)
    {
        ArrayList noiseArrayList = new ArrayList();
        noiseArrayList = ParseSomething.ParseFloats(newFloatString);
        StartCoroutine(PriorityChangeCoroutine((float)noiseArrayList[0], (float)noiseArrayList[1] ));
    }
    

    IEnumerator PriorityChangeCoroutine(float targetPriority, float secondsToWait)
    {
        vcam = TargetCamera.GetComponent<CinemachineVirtualCamera>();
        vcam.Priority = (int)targetPriority;
        yield return new WaitForSeconds(secondsToWait);
        vcam.Priority = initialPriority;

    }
}


