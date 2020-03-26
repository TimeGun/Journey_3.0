﻿using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UIElements;

public class CameraZone : MonoBehaviour
{
    public GameObject cameraTrigger;

    public bool FindCam1;
    public bool FindCam2;

    public string Cam1Name;
    public string Cam2Name;

    private bool playerInZone;
    public int targetPriority1;

    public int targetPriority2;

    //public int targetPriority3;
    public GameObject targetCamera1;

    public GameObject targetCamera2;

    //public GameObject targetCamera3;
    public float CameraTime = 0;
    public bool specialEvent;
    bool timedCameraChange = true;
    private int initialPriority1;

    private int initialPriority2;
    //private int initialPriority3;


    // Start is called before the first frame update

    private Collider col;

    void Start()
    {
        col = cameraTrigger.GetComponent<Collider>();

        if (targetCamera1.GetComponent<CinemachineVirtualCameraBase>() == null)
        {
            Debug.LogWarning(targetCamera1 + " is not a Camera Object");
        }

        if (targetCamera2.GetComponent<CinemachineVirtualCameraBase>() == null)
        {
            Debug.LogWarning(targetCamera2 + " is not a Camera Object");
        }

        if (FindCam1)
        {
            targetCamera1 = GameObject.Find(Cam1Name);
        }

        if (FindCam2)
        {
            targetCamera2 = GameObject.Find(Cam2Name);
        }

        if (targetCamera1 != null)
        {
            initialPriority1 = targetCamera1.GetComponent<CinemachineVirtualCameraBase>().Priority;
            if (targetPriority1 == 0)
            {
                Debug.LogWarning("Target Priority for target camera 1 is not set");
            }
        }

        if (targetCamera2 != null)
        {
            initialPriority2 = targetCamera2.GetComponent<CinemachineVirtualCameraBase>().Priority;
            if (targetPriority2 == 0)
            {
                Debug.LogWarning("Target Priority for target camera 2 is not set");
            }
        }


        //initialPriority3 = targetCamera3.GetComponent<CinemachineVirtualCameraBase>().Priority;
    }

    // Update is called once per frame
    void Update()
    {
        playerInZone = cameraTrigger.GetComponent<DetectPlayer>().PlayerInCollider;
//       Debug.Log(playerInZone);
        if (playerInZone)
        {
            if (CameraTime > 0 && specialEvent)
            {
                StartCoroutine(TimedPriorityChange());
                //return;
            }
            else if (specialEvent == false)
            {
                if (targetCamera1 != null)
                {
                    targetCamera1.GetComponent<CinemachineVirtualCameraBase>().Priority = targetPriority1;
                }

                if (targetCamera2 != null)
                {
                    targetCamera2.GetComponent<CinemachineVirtualCameraBase>().Priority = targetPriority2;
                }

                //targetCamera3.GetComponent<CinemachineVirtualCameraBase>().Priority = targetPriority3;
            }
        }
    }

    IEnumerator TimedPriorityChange()
    {
        while (timedCameraChange)
        {
            initialPriority1 = targetCamera1.GetComponent<CinemachineVirtualCameraBase>().Priority;
            initialPriority2 = targetCamera2.GetComponent<CinemachineVirtualCameraBase>().Priority;

            Debug.Log(initialPriority1);
            Debug.Log(initialPriority2);

            targetCamera1.GetComponent<CinemachineVirtualCameraBase>().Priority = targetPriority1;
            targetCamera2.GetComponent<CinemachineVirtualCameraBase>().Priority = targetPriority2;

            yield return new WaitForSeconds(CameraTime);
            timedCameraChange = false;
            targetCamera1.GetComponent<CinemachineVirtualCameraBase>().Priority = initialPriority1;
            targetCamera2.GetComponent<CinemachineVirtualCameraBase>().Priority = initialPriority2;
            timedCameraChange = false;
        }
    }


    private void OnDrawGizmos()
    {
        if (isActiveAndEnabled)
        {
            if (playerInZone)
            {
                Gizmos.color = new Color(1, 0, 0, 0.5f);
            }
            else
            {
                Gizmos.color = new Color(1, 0, 0, 0.1f);
            }


            if (col.GetType() == typeof(BoxCollider))
            {
                BoxCollider boxCol = (BoxCollider) col;
                Gizmos.DrawCube(cameraTrigger.transform.TransformPoint(boxCol.center),
                    Vector3.Scale(boxCol.size, cameraTrigger.transform.localScale));
                print(cameraTrigger.transform.TransformPoint(boxCol.center));
            }
            else if (col.GetType() == typeof(SphereCollider))
            {
                SphereCollider sphereCol = (SphereCollider) col;
                Gizmos.DrawSphere(sphereCol.center, sphereCol.radius);
            }
        }
    }
}