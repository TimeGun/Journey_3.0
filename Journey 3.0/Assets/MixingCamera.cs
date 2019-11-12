using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MixingCamera : MonoBehaviour
{
    public enum AxisEnum {X,Z,XZ};

    public Transform followTarget;
    public float initialBottomWeight = 20f;
    public AxisEnum axisToTrack;
    private bool playerDetected;
    private Vector3 playerStartPos;
    private float amountBelowZeroZ;
    private float amountAboveZeroZ;

    private CinemachineMixingCamera vcam;
    public GameObject cameraTrigger;
    
    void Start()
    {
        if (followTarget)
        {
           
            vcam = GetComponent<CinemachineMixingCamera>();
            vcam.m_Weight0 = initialBottomWeight;
            
        }
    }

    void Update()
    {
        playerDetected = cameraTrigger.GetComponent<DetectPlayer>().PlayerInCollider;
        Debug.Log(playerDetected);
        if (followTarget)
        {
            if (playerDetected)
            {
                
                if (playerStartPos == Vector3.zero)
                {
                    playerStartPos = followTarget.position;
                   /* if (followTarget.transform.position.z  0)
                    {
                        playerStartPos.z = playerStartPos.z 
                    }
                    if (followTarget.transform.position.z > 0)
                    {
                        amountAboveZeroZ = Mathf.Abs(followTarget.transform.position.z);
                    }*/
                    Debug.Log(playerStartPos);
                }
            }
        
            switch (axisToTrack)
            {
                case (AxisEnum.X):
                    
                    vcam.m_Weight1 = Mathf.Abs(followTarget.transform.position.x) - (playerStartPos.x);
                    break;
                case (AxisEnum.Z):
                    vcam.m_Weight1 = Mathf.Abs(followTarget.transform.position.z) - (playerStartPos.z);
                    break;
                case (AxisEnum.XZ):
                    vcam.m_Weight1 =
                        Mathf.Abs(Mathf.Abs(followTarget.transform.position.x) - (playerStartPos.x))+
                                  (Mathf.Abs(followTarget.transform.position.z) - (playerStartPos.x));
                    break;
            }
        }
    }
}
