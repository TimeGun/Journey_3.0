using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObject : MonoBehaviour
{
    private ObjectDetection _objectDetection;

    private InputSetUp _inputSetUp;

    private bool _interacting;
    
    [SerializeField] private Transform handPosition;

    private PlayerMovement _movement;


    [SerializeField] private float _turnSpeed;


    private IInteractible _interactingObj;

    void Start()
    {
        _inputSetUp = GetComponent<InputSetUp>();
        _objectDetection = GetComponent<ObjectDetection>();
        _movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (_inputSetUp.Controls.PlayerFreeMovement.Interact.triggered)
        {
            if (!_interacting && _objectDetection.Items.Count > 0)
            {
                GameObject obj =  ReturnCloserObject();
                _interactingObj = obj.GetComponent<IInteractible>();
                StartCoroutine(TurnToGrab(obj));
            }
            else if (_interacting)
            {
                _interactingObj.StopInteraction();
                _interactingObj = null;
                _interacting = false;
            }
        }
    }


    private GameObject ReturnCloserObject()
    {
        if (_objectDetection.Items.Count == 0)
        {
            return _objectDetection.Items[0];
        }
        else
        {
            GameObject closestObj = _objectDetection.Items[0];

            float closestDistance = float.MaxValue;
            
            for (int i = 0; i < _objectDetection.Items.Count; i++)
            {
                float thisDistance = Vector3.Distance(transform.position, _objectDetection.Items[i].transform.position);
                
                if (thisDistance < closestDistance)
                {
                    closestDistance = thisDistance;
                    closestObj = _objectDetection.Items[i];
                }
            }

            return closestObj;
        }


    }


    IEnumerator TurnToGrab(GameObject interactible)
    {
        _movement.enabled = false;

        Quaternion _targetRotation =
            Quaternion.LookRotation(interactible.transform.position - transform.position, Vector3.up);

        while (Vector3.Angle(transform.forward, interactible.transform.position - transform.position) > 20f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _turnSpeed);
            yield return new WaitForEndOfFrame();
        }
        
        _interactingObj.StartInteraction(handPosition);
        _movement.enabled = true;
        _interacting = true;
    }
}