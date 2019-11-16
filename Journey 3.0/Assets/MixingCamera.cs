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

    private int zPositionDirection = 1;
    private int xPositionDirection = 1;
    public bool zPositiveDirection;
    public bool xPositiveDirection;


    private CinemachineMixingCamera vcam;
    public GameObject cameraTrigger;
    
    void Start()
    {
        if (followTarget)
        {
           
            vcam = GetComponent<CinemachineMixingCamera>();
            vcam.m_Weight0 = initialBottomWeight;
            if (zPositiveDirection == false)
            {
                zPositionDirection = -1;
            }
            if (xPositiveDirection == false)
            {
                xPositionDirection = -1;
            }
        }
    }

    void Update()
    {
        playerDetected = cameraTrigger.GetComponent<DetectPlayer>().PlayerInCollider;
//        Debug.Log(playerDetected);
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
                    vcam.m_Weight1 = vcam.m_Weight1 * xPositionDirection;
                    break;
                case (AxisEnum.Z):
                    vcam.m_Weight1 = Mathf.Abs(followTarget.transform.position.z) - (playerStartPos.z);
                    vcam.m_Weight1 = vcam.m_Weight1 * zPositionDirection;
                    break;
                case (AxisEnum.XZ):
                    
                    float xWeight = Mathf.Abs(followTarget.transform.position.x - playerStartPos.x);
                    xWeight = xWeight * xPositionDirection;
                    float zWeight = Mathf.Abs(followTarget.transform.position.z - playerStartPos.z);
                    zWeight = zWeight * zPositionDirection;
                    Debug.Log(xWeight + "xweight");
                    Debug.Log(zWeight + "zweight");

                    vcam.m_Weight1 = (xWeight + zWeight) / 2;
                    
                        /*(Mathf.Abs(followTarget.transform.position.x) - (playerStartPos.x))+
                                  (Mathf.Abs(followTarget.transform.position.z) - (playerStartPos.z));
                    vcam.m_Weight1 = vcam.m_Weight1 * zPositionDirection;*/
                    break;
            }
        }
    }
}
