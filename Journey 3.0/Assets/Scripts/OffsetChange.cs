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

    private DetectPlayer _trigger1;
    private DetectPlayer _trigger2;


    void Start()
    {
        vcam = targetCamera.GetComponent<CinemachineVirtualCamera>();
        TD = vcam.GetCinemachineComponent<CinemachineTrackedDolly>();
        offset = TD.m_AutoDolly.m_PositionOffset;
        _trigger1 = cameraTrigger1.GetComponent<DetectPlayer>();
        _trigger2 = cameraTrigger2.GetComponent<DetectPlayer>();
    }

    void FixedUpdate()
    {
        if (_trigger1.PlayerEntered)
        {
            StopAllCoroutines();
            StartCoroutine(MoveToValue(positionOffset1)); //this is used to begin decreasing the value of the offset
        }
        else if (_trigger2.PlayerEntered)
        {
            StopAllCoroutines();
            StartCoroutine(MoveToValue(positionOffset2)); //this is used to begin increasing the value of the offset
        }
    }

    IEnumerator MoveToValue(float newPositionOffset)
    {
        while (TD.m_AutoDolly.m_PositionOffset > newPositionOffset + 0.01 ||
               TD.m_AutoDolly.m_PositionOffset < newPositionOffset - 0.01)
        {
            TD.m_AutoDolly.m_PositionOffset = Mathf.Lerp(TD.m_AutoDolly.m_PositionOffset, newPositionOffset,
                rateOfChange / 100 * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}