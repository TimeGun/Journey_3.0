using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;



//Finds the closest item of interest in front of the player
//Sets the look at target
public class InterestFinder : MonoBehaviour
{
    [SerializeField] private float radiusOfInterestVision;
    [SerializeField] private float distanceChecksPerSecond;
    [SerializeField] private float lookChangeSpeed;
    
    private bool newTargetFound;

    
    [SerializeField] private Transform[] transformsOfInterst;
    
    [SerializeField] private Transform lookTarget;
    [SerializeField] private Transform headPos;

    
    private List<Transform> closeItems = new List<Transform>();
    private List<Transform> infrontItems = new List<Transform>();

    private Vector3 lockedPosition;
    
    private Coroutine resetCoroutine;
    
    [SerializeField] private Rig LookRig; 
    

    void Start()
    {
        StartCoroutine(SetLookAtTarget());
        LookRig.weight = 0f;
        lockedPosition = lookTarget.position;
    }

    void Update()
    {
        lookTarget.position = lockedPosition;
    }

    IEnumerator SetLookAtTarget()
    {
        while (true)
        {
            Transform[] closeTransformsInFront = ReturnCloseInterestsInFront(transformsOfInterst);


            Transform potentialInterest = ClosestItem(closeTransformsInFront);

            if (potentialInterest != null)
            {
                if (lockedPosition != potentialInterest.position)
                {
                    
                    newTargetFound = true;

                    if (resetCoroutine != null)
                    {
                        StopCoroutine(resetCoroutine);
                    }

                    resetCoroutine = StartCoroutine(ResetRigWeight(potentialInterest.position));
                }
            }
            else
            {
                newTargetFound = false;
                
                if (resetCoroutine != null)
                {
                    StopCoroutine(resetCoroutine);
                }
                
                resetCoroutine = StartCoroutine(ResetRigWeight(Vector3.zero));
            }

            yield return new WaitForSeconds(1f / distanceChecksPerSecond);
        }
    }


    IEnumerator ResetRigWeight(Vector3 newTarget)
    {
        while (LookRig.weight > 0.1f)
        {
            yield return new WaitForEndOfFrame();
            LookRig.weight = Mathf.MoveTowards(LookRig.weight, 0f, Time.deltaTime * (lookChangeSpeed/2f));
        }

        if (newTargetFound)
        {
            lockedPosition = newTarget;
            while (LookRig.weight < 1)
            {
                LookRig.weight = Mathf.Lerp(LookRig.weight, 1f, Time.deltaTime * lookChangeSpeed);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    Transform [] ReturnCloseInterestsInFront(Transform[] interests)
    {
        closeItems.Clear();

        for (int i = 0; i < interests.Length; i++)
        {
            Vector3 directionToTarget = interests[i].position - transform.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            
            if(dSqrToTarget < Mathf.Pow(radiusOfInterestVision, 2f))
            {
                closeItems.Add(interests[i]);
            }
        }
        
        Transform[] instrestsInfront = ReturnInterestsInfront(closeItems.ToArray());

        
        
        
        return instrestsInfront;
    }

    Transform[] ReturnInterestsInfront(Transform[] interests)
    {
        infrontItems.Clear();
        for (int i = 0; i < interests.Length; i++)
        {
            
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = interests[i].position - transform.position;

            if (Vector3.Dot(forward, toOther) > 0)
            {
                infrontItems.Add(interests[i]);
            }
        }

        return infrontItems.ToArray();
    }

    Transform ClosestItem(Transform[] interests)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        for(int i = 0; i < interests.Length; i++)
        {
            Vector3 directionToTarget = interests[i].position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = interests[i];
            }
        }
     
        return bestTarget;
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawSphere(transform.position, radiusOfInterestVision);
        Gizmos.color = new Color(0, 1f, 0, 1f);
        Gizmos.DrawRay(headPos.position, headPos.forward.normalized * radiusOfInterestVision);
    }
}
