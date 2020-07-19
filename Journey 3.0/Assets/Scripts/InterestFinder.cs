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

    [SerializeField] private List<LookAtObject> objectsOfInterst = new List<LookAtObject>();
    [SerializeField] private List<LookAtObject> objectsOfInterstCinematic = new List<LookAtObject>();

    public List<LookAtObject> ObjectsOfInterstCinematic
    {
        get => objectsOfInterstCinematic;
        set => objectsOfInterstCinematic = value;
    }

    public List<LookAtObject> ObjectsOfInterst
    {
        get => objectsOfInterst;
        set => objectsOfInterst = value;
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
                closeTransformsInFront = ReturnCloseInterestsInFront(objectsOfInterstCinematic.ToArray());
            }
            else
            {
                closeTransformsInFront = ReturnCloseInterestsInFront(objectsOfInterst.ToArray());
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


    Transform[] ReturnCloseInterestsInFront(LookAtObject[] interests)
    {
        closeItems.Clear();

        for (int i = 0; i < interests.Length; i++)
        {
            Vector3 directionToTarget = interests[i]._transform.position - transform.position;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < Mathf.Pow(interests[i]._range, 2f))
            {
                closeItems.Add(interests[i]._transform);
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

    public void RemoveObject(LookAtObject toRemove, bool cinematic)
    {
        if (cinematic)
        {
            if (objectsOfInterstCinematic.Contains(toRemove))
            {
                objectsOfInterstCinematic.Remove(toRemove);
                lockedPosition = null;
            }
        }
        else
        {
            if (objectsOfInterst.Contains(toRemove))
            {
                objectsOfInterst.Remove(toRemove);
                lockedPosition = null;
            }
        }
    }

    public void AddObject(LookAtObject toAdd, bool cinematic)
    {
        if (cinematic)
        {
            if (!objectsOfInterstCinematic.Contains(toAdd))
            {
                objectsOfInterstCinematic.Add(toAdd);
                lockedPosition = null;
            }
        }
        else
        {
            if (!objectsOfInterst.Contains(toAdd))
            {
                objectsOfInterst.Add(toAdd);
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

[Serializable]
public class LookAtObject
{
    public Transform _transform;
    public float _range;

    public LookAtObject(Transform transform, float range)
    {
        _transform = transform;
        _range = range;
    }
}