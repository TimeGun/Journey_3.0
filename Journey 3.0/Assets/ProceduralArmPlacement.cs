using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

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
    
    void Start()
    {
        StartCoroutine(ArmPlacementRaycast());
    }

    void Update()
    {
        
    }

    IEnumerator ArmPlacementRaycast()
    {
        while (true)
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
                    
                        Quaternion handRot = AdjustHandRotation(rightRaycastHit.normal);

                        RightArmIK.Instance.SetProceduralTargetAndHint(rightRaycastHit.point + (rightRaycastHit.normal * _wallSeperationBuffer), handRot, 1f);
                    }
                }
                rightRayCol = Color.red;
                
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

                        LeftArmIK.Instance.SetProceduralTargetAndHint(leftRaycastHit.point + (leftRaycastHit.normal * _wallSeperationBuffer), handRot, 1f);
                    }
                }

                
            }
            else
            {
                LeftArmIK.Instance.TempUse = false;
                leftHandOn = false;
            }
            
            Debug.DrawRay(leftArmRay.origin, leftArmRay.direction * _wallDistanceCheck, leftRayCol, 0.1f);

            


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
