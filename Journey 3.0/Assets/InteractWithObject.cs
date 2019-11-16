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
                _interactingObj = _objectDetection.Items[0].GetComponent<IInteractible>();
                StartCoroutine(TurnToGrab(_objectDetection.Items[0]));
            }
            else if (_interacting)
            {
                _interactingObj.StopInteraction();
                _interactingObj = null;
                _interacting = false;
            }
        }
    }


    IEnumerator TurnToGrab(GameObject interactible)
    {
        _movement.enabled = false;

        Quaternion _targetRotation =
            Quaternion.LookRotation(interactible.transform.position - transform.position, Vector3.up);

        while (Vector3.Angle(transform.forward, interactible.transform.position - transform.position) > 10f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _turnSpeed);
            yield return new WaitForEndOfFrame();
        }
        
        _interactingObj.StartInteraction(handPosition);
        _movement.enabled = true;
        _interacting = true;
    }
}