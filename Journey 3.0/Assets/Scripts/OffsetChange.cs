using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;

public class OffsetChange : MonoBehaviour
{
    public GameObject targetCamera;
    private CinemachineVirtualCamera vcam;
    private CinemachineTrackedDolly TD;
    private float offset;
    //public float newOffset;
    //public GameObject cameraTrigger;
    private bool playerEntered;
    public float rateOfChange;
    private bool changeHasOccured;
    //public bool OneTimeChange;

    public float positionOffset1;
    public float positionOffset2;
    public GameObject cameraTrigger1;
    public GameObject cameraTrigger2;
    private bool firstTrigger;
    private bool secondTrigger; 

    
    // Start is called before the first frame update
    void Start()
    {
        vcam = targetCamera.GetComponent<CinemachineVirtualCamera>();
        TD = vcam.GetCinemachineComponent<CinemachineTrackedDolly>();
        offset = TD.m_AutoDolly.m_PositionOffset;
//        Debug.Log(offset);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        firstTrigger = cameraTrigger1.GetComponent<DetectPlayer>().PlayerEntered;   //Use this to trigger when  offset decreases
        secondTrigger = cameraTrigger2.GetComponent<DetectPlayer>().PlayerEntered;  //Use this to trigger when  offset increases
        
        if (firstTrigger)
        {
                   //this is used to begin decreasing the value of the offset
           StopAllCoroutines();
           StartCoroutine(MoveToValue(positionOffset1));
           //Debug.Log("Trigger 1");
        }
        else if (secondTrigger)
        {
           StopAllCoroutines();
           StartCoroutine(MoveToValue(positionOffset2)); 
            //Debug.Log("Trigger 2");//this is used to begin increasing the value of the offset
        }
        
    }

    IEnumerator MoveToValue(float newPositionOffset)
    {
        //Debug.Log("In Coroutine");

        while ( TD.m_AutoDolly.m_PositionOffset > newPositionOffset + 0.01 ||TD.m_AutoDolly.m_PositionOffset < newPositionOffset - 0.01)
        {
            //Debug.Log("In Coroutine");
            TD.m_AutoDolly.m_PositionOffset = Mathf.Lerp(TD.m_AutoDolly.m_PositionOffset, newPositionOffset, rateOfChange/100 * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
//            Debug.Log(TD.m_AutoDolly.m_PositionOffset);
//            Debug.Log(newOffset);
        }
    }

    /*void ChangeOffsetOnce()
    {
        if (playerEntered && changeHasOccured == false)
        {
            //TD.m_AutoDolly.m_PositionOffset = Mathf.Lerp(offset, newOffset, 0.1f);
            StartCoroutine(MoveToValue());
            changeHasOccured = true;
        }
        
    }

    void ChangeOffset()
    {
        if (playerEntered)
        {
            //TD.m_AutoDolly.m_PositionOffset = Mathf.Lerp(offset, newOffset, 0.1f);
            StartCoroutine(MoveToValue());
            
        }
    }*/
}

