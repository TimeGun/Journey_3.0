using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ChangeCameraWeight : MonoBehaviour
{
    public GameObject cameraTrigger;
    private bool playerDetected;
    private int initialPriority1;
    private int initialPriority2;
    public int targetPriority1;
    public int targetPriority2;
    public GameObject targetCamera1;
    public GameObject targetCamera2;

    
    // Start is called before the first frame update
    void Start()
    {
        initialPriority1 = targetCamera1.GetComponent<CinemachineVirtualCameraBase>().Priority;
        initialPriority2 = targetCamera2.GetComponent<CinemachineVirtualCameraBase>().Priority;
        if (targetPriority1 == 0 )
        {
            Debug.LogWarning("Target Priority for target camera 1 is not set");
            targetPriority1 = initialPriority1;
            
        }
        if (targetPriority2 == 0 )
        {
            Debug.LogWarning("Target Priority for target camera 2 is not set");
            targetPriority2 = initialPriority2;
            
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
        playerDetected = cameraTrigger.GetComponent<DetectPlayer>().PlayerDetected;
        if (playerDetected)
        {
            Debug.Log("yurt1");
            NewWeight();
        }
        else
        {
            OldWeight();
        }
        
    }

    void NewWeight()
    {
        targetCamera1.GetComponent<CinemachineVirtualCameraBase>().Priority = targetPriority1;
        targetCamera2.GetComponent<CinemachineVirtualCameraBase>().Priority = targetPriority2;
    }

    void OldWeight()
    {
        targetCamera1.GetComponent<CinemachineVirtualCameraBase>().Priority = initialPriority1;
        targetCamera2.GetComponent<CinemachineVirtualCameraBase>().Priority = initialPriority2;
    }
}
