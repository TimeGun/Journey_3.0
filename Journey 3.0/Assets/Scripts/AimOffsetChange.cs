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
    private float startOffset = 1000f;
    //public GameObject cameraTrigger1;
    //public GameObject cameraTrigger2;
    public GameObject cameraTrigger1;
    public GameObject cameraTrigger2;

    private bool playerEntered;
    private bool firstTrigger;
    private bool secondTrigger;


    private bool playerInZone1;
    private bool playerInZone2;

    public float rateOfChange;
    private bool runOnce = true;
    private bool afterTrigger;

    private bool changeBoolCoroutineRunning;

    private bool valueIncreasing;
    // Start is called before the first frame update
    void Start()
    {
        vcam = targetCamera.GetComponent<CinemachineVirtualCamera>();
        TD = vcam.GetCinemachineComponent<CinemachineTrackedDolly>();
        comp = vcam.GetCinemachineComponent<CinemachineComposer>();
        startOffset = comp.m_TrackedObjectOffset.y;
        //Need to figure out when to start coroutine
        //StartCoroutine(MoveToValue());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        firstTrigger = cameraTrigger1.GetComponent<DetectPlayer>().PlayerEntered;
        secondTrigger = cameraTrigger2.GetComponent<DetectPlayer>().PlayerEntered;


        if (firstTrigger)
        {
            afterTrigger = false;
        }
        else if (secondTrigger)
        {
            afterTrigger = true;
        }
       
        
            if (afterTrigger == false)
            {
                
                
               StartCoroutine(DecreaseValue());
              
            }
            else if (afterTrigger)
            {
                
                
              
                StartCoroutine(IncreaseValue());
            }


            if (changeBoolCoroutineRunning == false)
            {
              //StartCoroutine(ChangeBool());  
            }

            //afterTrigger = !afterTrigger;
            
            //Debug.Log("Offset: " + comp.m_TrackedObjectOffset.y);
            Debug.Log("StartOffset: " + startOffset);
            //Debug.Log("afterTrigger = " + afterTrigger);
            /*if (changeBoolCoroutineRunning == false)
            {
                StartCoroutine(ChangeBool());                
            }*/
            

        
        /*if (playerInZone1)
        {
            afterTrigger = false;
        }

        if (playerInZone2)
        {
            afterTrigger = true;
        }*/
    }
    
    IEnumerator DecreaseValue()
    {

        valueIncreasing = false;
            Debug.Log("In Coroutine");
            while (comp.m_TrackedObjectOffset.y > startOffset + 0.05f && valueIncreasing == false)
              {
                  Debug.Log("Decreasing Value");
                  comp.m_TrackedObjectOffset.y =
                        Mathf.Lerp(comp.m_TrackedObjectOffset.y, startOffset, rateOfChange / 1000);
                  yield return new WaitForSeconds(Time.deltaTime);

              }
            
        }

    IEnumerator IncreaseValue()
    {
        valueIncreasing = true;
         while (comp.m_TrackedObjectOffset.y < newOffset - 0.05f && valueIncreasing)
              {
                   Debug.Log("Increasing Value");
                   comp.m_TrackedObjectOffset.y =
                         Mathf.Lerp(comp.m_TrackedObjectOffset.y, newOffset, rateOfChange / 1000);
                   yield return new WaitForSeconds(Time.deltaTime);
              }
    }
    
    
    
    IEnumerator ChangeBool()
    {
        changeBoolCoroutineRunning = true;
        yield return new WaitForSeconds(Time.deltaTime * 2f);
        
        afterTrigger = !afterTrigger;
        Debug.Log("CR AfterTrigger = " + afterTrigger);
        changeBoolCoroutineRunning = false;
    }
}
