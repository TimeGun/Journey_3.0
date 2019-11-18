using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
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
    public float CameraTime = 0;
    public bool specialEvent;
    bool timedCameraChange = true;
    private int initialPriority1;
    private int initialPriority2;
    
    
    // Start is called before the first frame update
    void Start()
    {
        initialPriority1 = targetCamera1.GetComponent<CinemachineVirtualCameraBase>().Priority;
        initialPriority2 = targetCamera2.GetComponent<CinemachineVirtualCameraBase>().Priority;
        
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
                targetCamera1.GetComponent<CinemachineVirtualCameraBase>().Priority = targetPriority1;
                targetCamera2.GetComponent<CinemachineVirtualCameraBase>().Priority = targetPriority2;
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
}
