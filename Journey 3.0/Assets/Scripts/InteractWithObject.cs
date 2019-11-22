using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithObject : MonoBehaviour
{
    private ObjectDetection _objectDetection;

    private InputSetUp _inputSetUp;

    private bool _interacting;
    [SerializeField] private bool _nearRune;
    
    [SerializeField] private Transform handPosition;

    private PlayerMovement _movement;


    [SerializeField] private float _turnSpeed;


    private IInteractible _interactingObj;

    private IRune _rune;

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
        _nearRune = CheckNearRune();
        
        if (_inputSetUp.Controls.PlayerFreeMovement.Interact.triggered)
        {
            if (_nearRune && _objectDetection.Items.Count > 1)
            {
                _movement.ControllerVeclocity = Vector3.zero;
                _movement.enabled = false;

                GameObject rune = _rune.getGameObject();
                GameObject interactible;

                if (_interacting)
                {
                    interactible = _interactingObj.getGameObject();

                    GameObject[] temp = new GameObject[] {rune, interactible};
                    print(temp[1]);
                    _coroutine = StartCoroutine(UseRune(temp));
                }
                else
                {
                    interactible = ReturnCloserObject();

                    GameObject[] temp = new GameObject[] {rune, interactible};
                    _coroutine = StartCoroutine(UseRune(temp));
                }

                
            }else if (!_interacting && !_nearRune && _objectDetection.Items.Count > 0)
            {
                _interacting = true;
                _movement.ControllerVeclocity = Vector3.zero;
                _movement.enabled = false;
                


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
            else if (_interacting && !_nearRune)
            {
                _interactingObj.StopInteraction();
                _interactingObj = null;
                _interacting = false;
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }

                _coroutine = null;
            }
        }
    }

    private bool CheckNearRune()
    {
        if (_objectDetection.Items.Count == 0) return false;

        bool checker = false;
        for (int i = 0; i < _objectDetection.Items.Count; i++)
        {
            if (_objectDetection.Items[i].GetComponent<IRune>() != null)
            {
                _rune = _objectDetection.Items[i].GetComponent<IRune>();
                checker = true;
                break;
            }
        }

        return checker;
        
        
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
                if (_objectDetection.Items[i].GetComponent<IRune>() == null)
                {
                    float thisDistance = Vector3.Distance(transform.position, _objectDetection.Items[i].transform.position);
                
                    if (thisDistance < closestDistance)
                    {
                        closestDistance = thisDistance;
                        closestObj = _objectDetection.Items[i];
                    }
                }
            }
            print(closestObj);
            return closestObj;
        }


    }


    IEnumerator TurnToGrab(GameObject interactible)
    {

        Quaternion _targetRotation =
            Quaternion.LookRotation(interactible.transform.position - transform.position, Vector3.up);

        while (Quaternion.Angle(transform.rotation, _targetRotation) > 10f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _turnSpeed);

           yield return new WaitForEndOfFrame();
        }

        _interactingObj.StartInteraction(handPosition);
        
        yield return new WaitForEndOfFrame();

        _movement.enabled = true;
    }
    
    IEnumerator UseRune(GameObject[] runeAndInteractible)
    {

        /*Quaternion _targetRotation =
            Quaternion.LookRotation(interactible.transform.position - transform.position, Vector3.up);

        while (Quaternion.Angle(transform.rotation, _targetRotation) > 10f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _turnSpeed);

            yield return new WaitForEndOfFrame();
        }*/

        GameObject rune = runeAndInteractible[0];
        GameObject interactible = runeAndInteractible[1];

        
        interactible.GetComponent<ChangeSize>().ChangeSizeOfObject();
        
        yield return new WaitForEndOfFrame();

        _movement.enabled = true;
    }
    
    IEnumerator TurnToPush(GameObject interactible)
    {
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
        
        yield return new WaitForEndOfFrame();
        _movement.enabled = true;
        GetComponent<InputSetUp>().enabled = true;
    }
}