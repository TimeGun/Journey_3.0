using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ChangeCameraWeight : GradualLoader
{
    public GameObject cameraTrigger;
    private bool playerDetected;
    private bool playerEntered;
    private int initialPriority1;
    private int initialPriority2;
    public int targetPriority1;
    public int targetPriority2;
    public GameObject targetCamera1;
    public GameObject targetCamera2;

    private bool activated = false;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitUntil(() => initialised);
        initialPriority1 = targetCamera1.GetComponent<CinemachineVirtualCameraBase>().Priority;
        initialPriority2 = targetCamera2.GetComponent<CinemachineVirtualCameraBase>().Priority;
        if (targetPriority1 == 0)
        {
            Debug.LogWarning("Target Priority for target camera 1 is not set");
            targetPriority1 = initialPriority1;
        }

        if (targetPriority2 == 0)
        {
            Debug.LogWarning("Target Priority for target camera 2 is not set");
            targetPriority2 = initialPriority2;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (updateReady)
        {
            playerEntered = cameraTrigger.GetComponent<DetectPlayer>().PlayerEntered;
            Debug.Log("player entered" + playerEntered);
            playerDetected = cameraTrigger.GetComponent<DetectPlayer>().PlayerDetected;
            if (playerEntered)
            {
                activated = true;
            }


            if (playerDetected)
            {
                if (activated)
                {
                    NewWeight();
                    activated = false;
                }
            }
            else if (playerDetected == false)
            {
                OldWeight();
            }
        }
    }

    void NewWeight()
    {
        targetCamera1.GetComponent<CinemachineVirtualCameraBase>().Priority = targetPriority1;
        Debug.Log(targetPriority1);
        targetCamera2.GetComponent<CinemachineVirtualCameraBase>().Priority = targetPriority2;
        Debug.Log(targetPriority2);
    }

    void OldWeight()
    {
        targetCamera1.GetComponent<CinemachineVirtualCameraBase>().Priority = initialPriority1;
        targetCamera2.GetComponent<CinemachineVirtualCameraBase>().Priority = initialPriority2;
    }
}