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

    private Coroutine _coroutine;

    void Start()
    {
        _inputSetUp = GetComponent<InputSetUp>();
        _objectDetection = GetComponent<ObjectDetection>();
        _movement = GetComponent<PlayerMovement>();
        _coroutine = null;
    }

    void Update()
    {
        if (_inputSetUp.Controls.PlayerFreeMovement.Interact.triggered)
        {
            if (!_interacting && _objectDetection.Items.Count > 0)
            {
                _interacting = true;

                GameObject obj =  ReturnCloserObject();
                _interactingObj = obj.GetComponent<IInteractible>();


                var type = _interactingObj.GetType();
                
                if (type == typeof(PushObject))
                {
                    _coroutine = StartCoroutine(TurnToPush(obj));
                }else if (type == typeof(PickUpObject))
                {
                    _coroutine = StartCoroutine(TurnToGrab(obj));
                }
            }
            else if (_interacting)
            {
                _interactingObj.StopInteraction();
                _interactingObj = null;
                _interacting = false;
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
    }


    private GameObject ReturnCloserObject()
    {
        if (_objectDetection.Items.Count == 1)
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

        while (Quaternion.Angle(transform.rotation, _targetRotation) > 10f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _turnSpeed);

           yield return new WaitForEndOfFrame();
        }

        _interactingObj.StartInteraction(handPosition);
        _movement.enabled = true;
    }
    
    IEnumerator TurnToPush(GameObject interactible)
    {
        _movement.enabled = false;

        Quaternion _targetRotation =
            Quaternion.LookRotation(interactible.transform.position - transform.position, Vector3.up);
        
        _targetRotation.eulerAngles = new Vector3(transform.rotation.x, _targetRotation.eulerAngles.y, transform.rotation.z);

        while (Quaternion.Angle(transform.rotation, _targetRotation) > 10f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _turnSpeed);
            yield return new WaitForEndOfFrame();
        }

        transform.rotation = _targetRotation;
        
        _interactingObj.StartInteraction(handPosition);
        _movement.enabled = true;
    }
}