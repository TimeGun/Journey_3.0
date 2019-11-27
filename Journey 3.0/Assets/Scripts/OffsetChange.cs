﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OffsetChange : MonoBehaviour
{
    public GameObject targetCamera;
    private CinemachineVirtualCamera vcam;
    private CinemachineTrackedDolly TD;
    private float offset;
    public float newOffset;
    public GameObject cameraTrigger;
    private bool playerEntered;
    
    
    // Start is called before the first frame update
    void Start()
    {
        vcam = targetCamera.GetComponent<CinemachineVirtualCamera>();
        TD = vcam.GetCinemachineComponent<CinemachineTrackedDolly>();
        offset = TD.m_AutoDolly.m_PositionOffset;
        Debug.Log(offset);
    }

    // Update is called once per frame
    void Update()
    {
        playerEntered = cameraTrigger.GetComponent<DetectPlayer>().PlayerEntered;
        if (playerEntered)
        {
            //TD.m_AutoDolly.m_PositionOffset = Mathf.Lerp(offset, newOffset, 0.1f);
            StartCoroutine(MoveToValue());
        }
        
    }

    IEnumerator MoveToValue()
    {
        //Debug.Log("In Coroutine");

        while (TD.m_AutoDolly.m_PositionOffset > newOffset)
        {
            //Debug.Log("In Coroutine");
            TD.m_AutoDolly.m_PositionOffset = Mathf.Lerp(TD.m_AutoDolly.m_PositionOffset, newOffset, 0.02f);
            yield return new WaitForSeconds(Time.deltaTime);
            Debug.Log(TD.m_AutoDolly.m_PositionOffset);
            Debug.Log(newOffset);
        }
    }
}

