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
    public GameObject cameraTrigger;
    private bool playerEntered;
    public float rateOfChange;
    private bool playerHasEntered;

    private bool afterTrigger;
    // Start is called before the first frame update
    void Start()
    {
        vcam = targetCamera.GetComponent<CinemachineVirtualCamera>();
        TD = vcam.GetCinemachineComponent<CinemachineTrackedDolly>();
        comp = vcam.GetCinemachineComponent<CinemachineComposer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerEntered = cameraTrigger.GetComponent<DetectPlayer>().PlayerEntered;
        if (playerEntered)
        {
            playerHasEntered = true;
        }
        Debug.Log(playerEntered);
        if (playerHasEntered)
        {
        if (startOffset == 1000)
            {
                startOffset = comp.m_TrackedObjectOffset.y;
            }
            
            
            StartCoroutine(MoveToValue());
            Debug.Log("Offset: " + comp.m_TrackedObjectOffset.y);
            Debug.Log("StartOffset: " + startOffset);
            Debug.Log("afterTrigger = " + afterTrigger);
            afterTrigger = !afterTrigger;
            
        }
    }
    
    IEnumerator MoveToValue()
    {
        //Debug.Log("In Coroutine");
        if (afterTrigger == false)
        {
            while (comp.m_TrackedObjectOffset.y < newOffset)
            {
                //Debug.Log("In Coroutine");
                comp.m_TrackedObjectOffset.y = Mathf.Lerp(comp.m_TrackedObjectOffset.y, newOffset, rateOfChange / 100);
                yield return new WaitForSeconds(Time.deltaTime);
//            Debug.Log(TD.m_AutoDolly.m_PositionOffset);
//            Debug.Log(newOffset);
            }
        }
        else if (afterTrigger == true)
        {
            while (comp.m_TrackedObjectOffset.y > startOffset)
            {
                //Debug.Log("In Coroutine");
                comp.m_TrackedObjectOffset.y = Mathf.Lerp(comp.m_TrackedObjectOffset.y, startOffset, rateOfChange / 100);
                yield return new WaitForSeconds(Time.deltaTime);
//            Debug.Log(TD.m_AutoDolly.m_PositionOffset);
//            Debug.Log(newOffset);
            }
        }
    }
}
