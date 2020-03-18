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
    public GameObject ReferencePos;
    private Vector3 playerStartPos;
    private float amountBelowZeroZ;
    private float amountAboveZeroZ;

    private int zPositionDirection = 1;
    private int xPositionDirection = 1;
    public bool zPositiveDirection;
    public bool xPositiveDirection;
    public float minWeight;
    public float maxWeight;
    public bool testScene;
    public float finalCamMultiplier = 1f;



    private CinemachineMixingCamera vcam;
    public GameObject cameraTrigger;
    
    
    void Start()
    {
        
        
        if (testScene != true)
        {
            followTarget = API.GlobalReferences.PlayerRef.transform;
        }
            
        
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
        if (followTarget)
        {
            
 /*           if (vcam.m_Weight1 > 300 || vcam.m_Weight1 < -300)
            {
                Debug.LogError("Weight: " + vcam.m_Weight1 + " X Pos: " + followTarget.transform.position.x + " Y Pos: " + followTarget.transform.position.y);
            }
*/            if (playerDetected)
            {
                
                if (playerStartPos == Vector3.zero)
                {
                    playerStartPos = followTarget.position;
                    //GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    //marker.transform.position = playerStartPos;
                    //marker.GetComponent<Collider>().enabled = false;
                    Debug.Log(playerStartPos);
                }
                switch (axisToTrack)
                {
                    case (AxisEnum.X):
                    
                        vcam.m_Weight1 = Mathf.Abs(followTarget.transform.position.x) - (playerStartPos.x);
                        vcam.m_Weight1 = vcam.m_Weight1 * xPositionDirection * finalCamMultiplier;
                        break;
                    case (AxisEnum.Z):
                        vcam.m_Weight1 = Mathf.Abs(followTarget.transform.position.z) - (playerStartPos.z);
                        vcam.m_Weight1 = vcam.m_Weight1 * zPositionDirection * finalCamMultiplier;
                        break;
                    case (AxisEnum.XZ):
                    
                        float xWeight = Mathf.Abs(followTarget.transform.position.x - playerStartPos.x);
                        xWeight = xWeight * xPositionDirection;
                        float zWeight = Mathf.Abs(followTarget.transform.position.z - playerStartPos.z);
                        zWeight = zWeight * zPositionDirection * finalCamMultiplier;
                        if (followTarget.position.z > playerStartPos.z && zPositiveDirection)
                        {
                            zWeight = minWeight;
                        }                        
                        vcam.m_Weight1 = (xWeight + zWeight) / 2;
                        break;
                }
            }
          }
    }
}
