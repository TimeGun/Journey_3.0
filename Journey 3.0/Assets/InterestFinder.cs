using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class InterestFinder : MonoBehaviour
{
    [SerializeField] private float radiusOfInterestVision;
    [SerializeField] private float distanceChecksPerSecond;
    [SerializeField] private Transform[] transformsOfInterst;
    
    [SerializeField]private List<Transform> closeItems = new List<Transform>();
    [SerializeField]private List<Transform> infrontItems = new List<Transform>();

    [SerializeField] private Transform lookTarget;

    private Vector3 lockedPosition;

    [SerializeField]private Rig LookRig;

    private bool newTargetFound;

    private Coroutine reseter;

    [SerializeField]
    private float lookChangeSpeed;
    

    void Start()
    {
        StartCoroutine(SetLookAtTarget());
        LookRig.weight = 0f;
        lockedPosition = lookTarget.position;
    }

    void Update()
    {
        if(newTargetFound)
        lookTarget.position = lockedPosition;
    }

    IEnumerator SetLookAtTarget()
    {
        while (true)
        {
            Transform[] closeTransforms = ReturnCloseInterests(transformsOfInterst);

            Transform[] instrestsInfront = ReturnInterestsInfront(closeTransforms);

            Transform potentialInterest = ClosestItem(instrestsInfront);

            if (potentialInterest != null)
            {
                if (lockedPosition != potentialInterest.position)
                {
                    
                    newTargetFound = true;

                    if (reseter != null)
                    {
                        StopCoroutine(reseter);
                    }

                    reseter = StartCoroutine(ResetRigWeight(potentialInterest.position));
                }
            }
            else
            {
                newTargetFound = false;
                
                if (reseter != null)
                {
                    StopCoroutine(reseter);
                }
                
                reseter = StartCoroutine(ResetRigWeight(Vector3.zero));
            }

            yield return new WaitForSeconds(1f / distanceChecksPerSecond);
        }
    }


    IEnumerator ResetRigWeight(Vector3 newTarget)
    {
        while (LookRig.weight > 0.1f)
        {
            yield return new WaitForEndOfFrame();
            LookRig.weight = Mathf.Lerp(LookRig.weight, 0f, Time.deltaTime * lookChangeSpeed);
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

    Transform [] ReturnCloseInterests(Transform[] interests)
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
        
        return closeItems.ToArray();
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
    }
}
