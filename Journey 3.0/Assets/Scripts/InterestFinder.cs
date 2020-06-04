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

    [SerializeField] private List<Transform> transformsOfInterst = new List<Transform>();
    [SerializeField] private List<Transform> transformsOfInterstCinematic = new List<Transform>();

    public List<Transform> TransformsOfInterstCinematic
    {
        get => transformsOfInterstCinematic;
        set => transformsOfInterstCinematic = value;
    }

    public List<Transform> TransformsOfInterst
    {
        get => transformsOfInterst;
        set => transformsOfInterst = value;
    }

    [SerializeField] private Transform lookTarget;
    [SerializeField] private Transform headPos;


    private List<Transform> closeItems = new List<Transform>();
    private List<Transform> infrontItems = new List<Transform>();

    private Transform lockedPosition;

    private Coroutine resetCoroutine;

    [SerializeField] private Rig LookRig;

    public GameObject interactingObj;

    public static InterestFinder instance;

    [SerializeField] private PlayerMovement _playerMovement;
    
    public bool _debug;


    void Start()
    {
        instance = this;
        StartCoroutine(SetLookAtTarget());
        LookRig.weight = 0f;
        if (lockedPosition != null)
            lockedPosition.position = lookTarget.position;
    }

    void Update()
    {
        if (lockedPosition != null)
        {
            lookTarget.position = lockedPosition.position;
        }
    }

    IEnumerator SetLookAtTarget()
    {
        while (true)
        {
            Transform[] closeTransformsInFront;
            if (_playerMovement.cinematicMoment)
            {
                closeTransformsInFront = ReturnCloseInterestsInFront(transformsOfInterstCinematic.ToArray());
            }
            else
            {
                closeTransformsInFront = ReturnCloseInterestsInFront(transformsOfInterst.ToArray());
            }
            
            
            Transform potentialInterest = ClosestItem(closeTransformsInFront);

            if (potentialInterest != null)
            {
                if (lockedPosition != potentialInterest)
                {
                    
                    newTargetFound = true;

                    if (resetCoroutine != null)
                    {
                        StopCoroutine(resetCoroutine);
                    }

                    resetCoroutine = StartCoroutine(ResetRigWeight(potentialInterest));
                }
            }
            else
            {
                newTargetFound = false;

                if (resetCoroutine != null)
                {
                    StopCoroutine(resetCoroutine);
                }

                resetCoroutine = StartCoroutine(ResetRigWeight(transform));
            }

            yield return new WaitForSeconds(1f / distanceChecksPerSecond);
        }
    }


    IEnumerator ResetRigWeight(Transform newTarget)
    {
        while (LookRig.weight > 0.1f)
        {
            lockedPosition = null;
            yield return new WaitForEndOfFrame();
            LookRig.weight = Mathf.MoveTowards(LookRig.weight, 0f, Time.deltaTime * (lookChangeSpeed / 2f));
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


    Transform[] ReturnCloseInterestsInFront(Transform[] interests)
    {
        closeItems.Clear();

        for (int i = 0; i < interests.Length; i++)
        {
            Vector3 directionToTarget = interests[i].position - transform.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < Mathf.Pow(radiusOfInterestVision, 2f))
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
                if (interactingObj == null)
                {
                    infrontItems.Add(interests[i]);
                }
                else
                {
                    if (interactingObj.transform != interests[i])
                    {
                        infrontItems.Add(interests[i]);
                    }
                }

            }
        }

        return infrontItems.ToArray();
    }

    Transform ClosestItem(Transform[] interests)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        for (int i = 0; i < interests.Length; i++)
        {
            
            if (interests[i] != null && interests[i].gameObject.activeSelf)
            {
                Vector3 directionToTarget = interests[i].position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = interests[i];
                }
            }
        }

        return bestTarget;
    }

    public void RemoveObject(Transform toRemove, bool cinematic)
    {
        if (cinematic)
        {
            if (!transformsOfInterstCinematic.Contains(toRemove))
            {
                transformsOfInterstCinematic.Remove(toRemove);
                lockedPosition = null;
            }
        }
        else
        {
            if (!transformsOfInterst.Contains(toRemove))
            {
                transformsOfInterst.Remove(toRemove);
                lockedPosition = null;
            }
        }

    }
    
    public void AddObject(Transform toAdd, bool cinematic)
    {
        if (cinematic)
        {
            if (!transformsOfInterstCinematic.Contains(toAdd))
            {
                transformsOfInterstCinematic.Add(toAdd);
                lockedPosition = null;
            }
        }
        else
        {
            if (!transformsOfInterst.Contains(toAdd))
            {
                transformsOfInterst.Add(toAdd);
                lockedPosition = null;
            }
        }

        
    }


    private void OnDrawGizmos()
    {
        if (_debug)
        {
            Gizmos.color = new Color(1, 0, 0, 0.2f);
            Gizmos.DrawSphere(transform.position, radiusOfInterestVision);
            Gizmos.color = new Color(0, 1f, 0, 1f);
            Gizmos.DrawRay(headPos.position, headPos.up.normalized * radiusOfInterestVision);
        }
    }
}