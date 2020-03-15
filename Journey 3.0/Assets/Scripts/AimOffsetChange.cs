using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimOffsetChange : MonoBehaviour
{
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

    private bool changeBoolCoroutineRunning;

    private bool valueIncreasing;       
    // Start is called before the first frame update
    void Start()
    {
        vcam = targetCamera.GetComponent<CinemachineVirtualCamera>();                
        TD = vcam.GetCinemachineComponent<CinemachineTrackedDolly>();
        comp = vcam.GetCinemachineComponent<CinemachineComposer>();                //Need to use this to access the LookAt target
        startOffset = comp.m_TrackedObjectOffset.y;                                //Assign start offset to the offset set in the inspector when it starts
      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        firstTrigger = cameraTrigger1.GetComponent<DetectPlayer>().PlayerEntered;   //Use this to trigger when y offset decreases
        secondTrigger = cameraTrigger2.GetComponent<DetectPlayer>().PlayerEntered;  //Use this to trigger when y offset increases


        if (firstTrigger)
        {
            afterTrigger = false;        //this is used to begin decreasing the value of the offset
            
        }
        else if (secondTrigger)
        {
            afterTrigger = true;        //this is used to begin increasing the value of the offset
        }
       
        
            if (afterTrigger == false)
            {
               StartCoroutine(DecreaseValue());
            }
            else if (afterTrigger)
            {
                StartCoroutine(IncreaseValue());
            }
    }
    
    IEnumerator DecreaseValue()
    {

        valueIncreasing = false;        //used to ensure offset value is not increasing in IncreaseValue() while it is decreasing here
          
            while (comp.m_TrackedObjectOffset.y > startOffset + 0.05f && valueIncreasing == false)          //+0.05 is used because offset will never reach the startoffset value
              {
                  comp.m_TrackedObjectOffset.y =
                        Mathf.Lerp(comp.m_TrackedObjectOffset.y, startOffset, rateOfChange / 1000);        //lerp y offset of the lookat target to the startoffset value over time
                  yield return new WaitForSeconds(Time.deltaTime);            //wait for as frame to avoid changing value instantly

              }
            
        }

    IEnumerator IncreaseValue()
    {
        valueIncreasing = true;
         while (comp.m_TrackedObjectOffset.y < newOffset - 0.05f && valueIncreasing)                        //-0.05 is used because offset will never reach newoffset value
              {
                   comp.m_TrackedObjectOffset.y =
                         Mathf.Lerp(comp.m_TrackedObjectOffset.y, newOffset, rateOfChange / 1000);           //lerp y offset of the lookat target to the newoffset value over time
                   yield return new WaitForSeconds(Time.deltaTime);            //wait for as frame to avoid changing value instantly
              }
    }
    
    
    
    IEnumerator ChangeBool(int framesToWait)            //not used - Used to change a bool - waits for an amount of time before this can be called again
    {
        changeBoolCoroutineRunning = true;
        yield return new WaitForSeconds(Time.deltaTime * framesToWait);
        
        afterTrigger = !afterTrigger;
        Debug.Log("CR AfterTrigger = " + afterTrigger);
        changeBoolCoroutineRunning = false;
    }
}
