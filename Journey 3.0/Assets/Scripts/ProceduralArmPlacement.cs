using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ProceduralArmPlacement : MonoBehaviour
{
    [SerializeField] private Transform _chestHeight;

    [SerializeField] private float _wallDistanceCheck;
    
    [SerializeField] [Range(0, 1)] private float armSeperationFloat;

    [SerializeField] private LayerMask _wallMask;

    [SerializeField] [Range(0, 1)] private float _wallSeperationBuffer;

    [SerializeField] private float angleAdjustment1 = 90f;
    [SerializeField] private float angleAdjustment2 = -90f;

    [SerializeField] private float raycastPerSecond = 3f;

    private bool setRightArm;

    private bool setLeftArm;


    public bool oneHanded;
    
    private bool rightHandOn;
    private bool leftHandOn;

    public bool pause = false;
    
    void Start()
    {
        StartCoroutine(ArmPlacementRaycast());
    }


    IEnumerator ArmPlacementRaycast()
    {
        while (true)
        {
            if (!pause)
            {
                 //raycast right arm
            Ray rightArmRay = new Ray(_chestHeight.position + (transform.right * armSeperationFloat), _chestHeight.forward);
            RaycastHit rightRaycastHit;
            
            
            Color rightRayCol = Color.green;

            if (Physics.Raycast(rightArmRay, out rightRaycastHit, _wallDistanceCheck, _wallMask))
            {
                if (oneHanded && !leftHandOn || !oneHanded)
                {
                    if (!RightArmIK.Instance.InUse)
                    {
                        print("set right arm");
                        RightArmIK.Instance.TempUse = true;
                        rightHandOn = true;
                        
                        

                        RightArmIK.Instance.SetProceduralTargetAndHint(rightRaycastHit.point + (rightRaycastHit.normal * _wallSeperationBuffer), rightRaycastHit.normal, 1f);
                    }
                }
                rightRayCol = Color.red;
                Debug.DrawLine(rightRaycastHit.point, rightRaycastHit.point + (rightRaycastHit.normal * 100f));
                
            }
            else
            {
                RightArmIK.Instance.TempUse = false;
                rightHandOn = false;
            }

            Debug.DrawRay(rightArmRay.origin, rightArmRay.direction * _wallDistanceCheck, rightRayCol, 0.05f);
            
            
            
            
            //raycast left arm
            Ray leftArmRay = new Ray(_chestHeight.position - (transform.right * armSeperationFloat), _chestHeight.forward);
            RaycastHit leftRaycastHit;
            
            
            Color leftRayCol = Color.green;
            
            if (Physics.Raycast(leftArmRay, out leftRaycastHit, _wallDistanceCheck, _wallMask))
            {
                if (oneHanded && !rightHandOn || !oneHanded)
                {
                    leftRayCol = Color.red;
                    if (!LeftArmIK.Instance.InUse)
                    {
                        print("set left arm");
                        LeftArmIK.Instance.TempUse = true;
                        leftHandOn = true;
                        
                        Quaternion handRot = AdjustHandRotation(leftRaycastHit.normal);

                        LeftArmIK.Instance.SetProceduralTargetAndHint(leftRaycastHit.point + (leftRaycastHit.normal * _wallSeperationBuffer), leftRaycastHit.normal, 1f);
                    }
                }

                Debug.DrawLine(leftRaycastHit.point, leftRaycastHit.point + (leftRaycastHit.normal * 100f));

            }
            else
            {
                LeftArmIK.Instance.TempUse = false;
                leftHandOn = false;
            }
            
            Debug.DrawRay(leftArmRay.origin, leftArmRay.direction * _wallDistanceCheck, leftRayCol, 0.1f);
            }

           

            


            yield return new WaitForSeconds(1f/ raycastPerSecond);
        }
    }


    Quaternion AdjustHandRotation(Vector3 wallNormal)
    {
        Quaternion handRot = Quaternion.Inverse(Quaternion.LookRotation(wallNormal));
        
        handRot *= Quaternion.AngleAxis(angleAdjustment1, Vector3.right);
        handRot *= Quaternion.AngleAxis(angleAdjustment2, Vector3.up);

        return handRot;
    }
}
