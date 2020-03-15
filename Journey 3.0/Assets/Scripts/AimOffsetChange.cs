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
    public GameObject cameraTrigger1;
    public GameObject cameraTrigger2;
    //public GameObject cameraTrigger;

    //private bool playerEntered;

    private bool playerInZone1;
    private bool playerInZone2;

    public float rateOfChange;
    private bool runOnce = true;
    private bool afterTrigger;

    private bool changeBoolCoroutineRunning;
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
    void Update()
    {
        //playerEntered = cameraTrigger.GetComponent<DetectPlayer>().PlayerEntered;
        playerInZone1 = cameraTrigger1.GetComponent<DetectPlayer>().PlayerInCollider;
        playerInZone2 = cameraTrigger2.GetComponent<DetectPlayer>().PlayerInCollider;
        Debug.Log("Zone1" + playerInZone1);
        Debug.Log("Zone2" + playerInZone2);

        /*if (playerEntered)
        {
            if (startOffset == 1000)
            {
                startOffset = comp.m_TrackedObjectOffset.y;
            }
            
            
            StartCoroutine(MoveToValue());
            //Debug.Log("Offset: " + comp.m_TrackedObjectOffset.y);
            Debug.Log("StartOffset: " + startOffset);
            //Debug.Log("afterTrigger = " + afterTrigger);
            if (changeBoolCoroutineRunning == false)
            {
                StartCoroutine(ChangeBool());                
            }
            

        }*/

        if (playerInZone1)
        {
            afterTrigger = false;
        }

        if (playerInZone2)
        {
            afterTrigger = true;
        }
    }
    
    IEnumerator MoveToValue()
    {
        while (true)
        {


            //Debug.Log("In Coroutine");
            if (afterTrigger)
            {
                while (comp.m_TrackedObjectOffset.y < newOffset - 0.05f)
                {
                    Debug.Log("Increasing Value");
                    comp.m_TrackedObjectOffset.y =
                        Mathf.Lerp(comp.m_TrackedObjectOffset.y, newOffset, rateOfChange / 100);
                    yield return new WaitForSeconds(Time.deltaTime);
//            Debug.Log(TD.m_AutoDolly.m_PositionOffset);
//            Debug.Log(newOffset);
                }
            }
            else if (afterTrigger == false)
            {
                while (comp.m_TrackedObjectOffset.y > startOffset + 0.05f)
                {
                    Debug.Log("Decreasing Value");
                    comp.m_TrackedObjectOffset.y =
                        Mathf.Lerp(comp.m_TrackedObjectOffset.y, startOffset, rateOfChange / 100);
                    yield return new WaitForSeconds(Time.deltaTime);
//            Debug.Log(TD.m_AutoDolly.m_PositionOffset);
//            Debug.Log(newOffset);
                }
            }
        }


    }
    IEnumerator ChangeBool()
    {
        changeBoolCoroutineRunning = true;
        yield return new WaitForSeconds(Time.deltaTime * 60f);
        
        afterTrigger = !afterTrigger;
        Debug.Log("CR AfterTrigger = " + afterTrigger);
        changeBoolCoroutineRunning = false;
    }
}
