using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObject : MonoBehaviour
{
    private ObjectDetection _objectDetection;

    private InputSetUp _inputSetUp;

    private bool _grabbing;


    private GameObject _itemHeld;

    [SerializeField] private Transform handPosition;

    private PlayerMovement _movement;


    [SerializeField] private float _turnSpeed;

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
            if (!_grabbing && _objectDetection.Items.Count > 0)
            {
                IPickup itemToPickUP = _objectDetection.Items[0].GetComponent<IPickup>();

                itemToPickUP.GetPickedUp(handPosition);
            }
            else if (_grabbing)
            {
                _itemHeld.GetComponent<IPickup>().GetDropped();
                _itemHeld = null;
                _grabbing = false;
            }
        }

        if (_grabbing)
        {
            _itemHeld.transform.position = handPosition.position;
        }
    }

    public void SetGrabObject(GameObject obj)
    {
        _itemHeld = obj;
        StartCoroutine(TurnToGrab());
    }


    IEnumerator TurnToGrab()
    {
        _movement.enabled = false;

        Quaternion _targetRotation =
            Quaternion.LookRotation(_itemHeld.transform.position - transform.position, Vector3.up);

        while (Vector3.Angle(transform.forward, _itemHeld.transform.position - transform.position) > 10f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _turnSpeed);
            yield return new WaitForEndOfFrame();
        }

        _movement.enabled = true;
        _grabbing = true;
    }
}