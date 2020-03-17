using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralArmPlacement : MonoBehaviour
{
    [SerializeField] private Transform _chestHeight;

    [SerializeField] private float _wallDistanceCheck;
    
    [SerializeField] [Range(0, 1)] private float armSeperationFloat;

    [SerializeField] private LayerMask _wallMask;
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
                rightRayCol = Color.red;
            }

            Debug.DrawRay(rightArmRay.origin, rightArmRay.direction * _wallDistanceCheck, rightRayCol, 0.1f);
            
            
            
            
            //raycast left arm
            Ray leftArmRay = new Ray(_chestHeight.position - (transform.right * armSeperationFloat), _chestHeight.forward);
            RaycastHit leftRaycastHit;
            
            
            Color leftRayCol = Color.green;
            
            if (Physics.Raycast(leftArmRay, out leftRaycastHit, _wallDistanceCheck, _wallMask))
            {
                leftRayCol = Color.red;
            }
            
            Debug.DrawRay(leftArmRay.origin, leftArmRay.direction * _wallDistanceCheck, leftRayCol, 0.1f);

            


            yield return new WaitForEndOfFrame();
        }
    }
}
