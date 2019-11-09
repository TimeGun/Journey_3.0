using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public GameObject cameraTrigger;

    private bool playerInZone;
    public int targetPriority1;
    public int targetPriority2;
    public GameObject targetCamera1;
    public GameObject targetCamera2;
    // Start is called before the first frame update
    void Start()
    {
        if (targetPriority1 == 0 )
        {
            Debug.LogWarning("Target Priority for target camera 1 is not set");                        
        }
        if (targetPriority2 == 0 )
        {
            Debug.LogWarning("Target Priority for target camera 2 is not set");                        
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerInZone = cameraTrigger.GetComponent<DetectPlayer>().PlayerInCollider;
       
        if (playerInZone)
        {
            
            targetCamera1.GetComponent<CinemachineVirtualCameraBase>().Priority = targetPriority1;
            targetCamera2.GetComponent<CinemachineVirtualCameraBase>().Priority = targetPriority2;
        }
    }
}
