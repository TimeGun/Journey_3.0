using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Cinemachine;

public class AimOffsetChange : GradualLoader
{
    public enum AxisEnum {x,y,z};

    private string axisString;
    public AxisEnum axisToTrack;
    public GameObject targetCamera;
    private CinemachineVirtualCamera vcam;
    private CinemachineTrackedDolly TD;
    private CinemachineComposer comp;
    public float newOffset;
    private float startOffset;
    public GameObject cameraTrigger1;
    public GameObject cameraTrigger2;

    private bool playerEntered;
    private bool firstTrigger;
    private bool secondTrigger; 
    public float rateOfChange;
    private bool afterTrigger;
    private float currentOffset;
    private float targetOffset;

    private bool inIVCoroutine;
    private bool inDVCoroutine;
    public float firstOffset;
    

    private bool changeBoolCoroutineRunning;

    private bool valueIncreasing;       
    // Start is called before the first frame update
    public override void Awake()
    {
        print("Called Awake");
        base.Awake();
    }
    
    
    IEnumerator Start()
    {
        yield return new WaitUntil(() => initialised);
        
        vcam = targetCamera.GetComponent<CinemachineVirtualCamera>();                
        TD = vcam.GetCinemachineComponent<CinemachineTrackedDolly>();
        comp = vcam.GetCinemachineComponent<CinemachineComposer>();                //Need to use this to access the LookAt target
        CheckAxis();
        startOffset = firstOffset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (initialised)
        {
            CheckAxis();
            firstTrigger = cameraTrigger1.GetComponent<DetectPlayer>().PlayerEntered;   //Use this to trigger when  offset decreases
            secondTrigger = cameraTrigger2.GetComponent<DetectPlayer>().PlayerEntered;  //Use this to trigger when  offset increases
  
            //Debug.Log("Start Offset: " + startOffset);
            //Debug.Log("New Offset: " + newOffset);
            //Debug.Log("Current Offset: " + currentOffset);
        
            if (firstTrigger)
            {
                afterTrigger = false;        //this is used to begin decreasing the value of the offset
            
            }
            else if (secondTrigger)
            {
                afterTrigger = true;    //this is used to begin increasing the value of the offset
            }

        
            if (afterTrigger == false && firstTrigger)
            {
                StartCoroutine(DecreaseValue());
            }
            else if (afterTrigger && secondTrigger)
            {

                StartCoroutine(IncreaseValue());
            }
        }
    }
    
    IEnumerator DecreaseValue()
    {
        
        valueIncreasing = false;        //used to ensure offset value is not increasing in IncreaseValue() while it is decreasing here
        if (startOffset > newOffset)        //e.g SO = 0, NO = -28
        {
            
            while (currentOffset > newOffset + 0.05f && valueIncreasing == false)                        //-0.05 is used because offset will never reach newoffset value
            {
                    
                targetOffset = newOffset;
                AssignAxis();                             //lerp offset of the lookat target to the newoffset value over time
                yield return new WaitForSeconds(Time.deltaTime);            //wait for as frame to avoid changing value instantly
            }
        }
        else                 //e.g SO = 0, NO = 3.5
        {
            while (currentOffset > startOffset + 0.05f && valueIncreasing == false)          //+0.05 is used because offset will never reach the startoffset value
            {
                targetOffset = startOffset;
                AssignAxis();                                                     //lerp offset of the lookat target to the startoffset value over time
                yield return new WaitForSeconds(Time.deltaTime);            //wait for as frame to avoid changing value instantly

            }
        }
            
    }

    IEnumerator IncreaseValue()
    {
        
        valueIncreasing = true;
        if (startOffset > newOffset)
        {
            //Debug.Log("please dont Increase");      
            while (currentOffset < startOffset - 0.05f && valueIncreasing)          //+0.05 is used because offset will never reach the startoffset value
            {
                targetOffset = startOffset;
                AssignAxis();                                                     //lerp offset of the lookat target to the startoffset value over time
                yield return new WaitForSeconds(Time.deltaTime);            //wait for as frame to avoid changing value instantly

            }
        }
        else
        {
            while (currentOffset < newOffset - 0.05f && valueIncreasing)                        //-0.05 is used because offset will never reach newoffset value
            {
                targetOffset = newOffset;
                AssignAxis();                             //lerp offset of the lookat target to the newoffset value over time
                yield return new WaitForSeconds(Time.deltaTime);            //wait for as frame to avoid changing value instantly
            }
        }
         

        
    }
    
    
    
    IEnumerator ChangeBool(int framesToWait)            //not used - Used to change a bool - waits for an amount of time before this can be called again
    {
        changeBoolCoroutineRunning = true;
        yield return new WaitForSeconds(Time.deltaTime * framesToWait);
        
        afterTrigger = !afterTrigger;
  
        changeBoolCoroutineRunning = false;
    }

    void CheckAxis()
    {
        switch (axisToTrack)
        {
            case (AxisEnum.x):
            {
                axisString = "x";
                currentOffset = comp.m_TrackedObjectOffset.x;
                break;
            }
            case (AxisEnum.y):
            {
                axisString = "y";
                currentOffset = comp.m_TrackedObjectOffset.y;
                break;
            }
            case (AxisEnum.z):
            {
                axisString = "z";
                currentOffset = comp.m_TrackedObjectOffset.z;
                break;
            }
        }
    }

    void AssignAxis()
    {
        
        /*if (valueIncreasing)
        {
            targetOffset = newOffset;
        }
        else if (valueIncreasing == false)
        {
            targetOffset = startOffset;
        }*/
        
        switch (axisToTrack)
        {
            case (AxisEnum.x):
            {
                comp.m_TrackedObjectOffset.x =
                    Mathf.Lerp(comp.m_TrackedObjectOffset.x, targetOffset, rateOfChange / 1000); 
                break;
            }
            case (AxisEnum.y):
            {
                comp.m_TrackedObjectOffset.y =
                    Mathf.Lerp(comp.m_TrackedObjectOffset.y, targetOffset, rateOfChange / 1000); 
                break;
            }
            case (AxisEnum.z):
            {
                comp.m_TrackedObjectOffset.z =
                    Mathf.Lerp(comp.m_TrackedObjectOffset.z, targetOffset, rateOfChange / 1000); 
                break;
            }
        }
    }
    
    public override void EnqueThis()
    {
        
        base.EnqueThis();
    }

    public override void InitialiseThis()
    {
        base.InitialiseThis();
    }
}
