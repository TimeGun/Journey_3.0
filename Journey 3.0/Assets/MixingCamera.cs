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
        playerDetected = cameraTrigger.GetComponent<DetectPlayer>().PlayerDetected;
        
        if (followTarget)
        {
            if (playerDetected)
            {
                //Debug.Log("yurt2");
                if (playerStartPos == Vector3.zero)
                {
                    playerStartPos = followTarget.position;
                }
            }
//            Debug.Log(playerStartPos);
            switch (axisToTrack)
            {
                case (AxisEnum.X):
                    vcam.m_Weight1 = Mathf.Abs(followTarget.transform.position.x) - Mathf.Abs(playerStartPos.x);
                    break;
                case (AxisEnum.Z):
                    vcam.m_Weight1 = Mathf.Abs(followTarget.transform.position.z) - Mathf.Abs(playerStartPos.z);
                    break;
                case (AxisEnum.XZ):
                    vcam.m_Weight1 =
                        Mathf.Abs(Mathf.Abs(followTarget.transform.position.x) +
                                  Mathf.Abs(followTarget.transform.position.z));
                    break;
            }
        }
    }
}
