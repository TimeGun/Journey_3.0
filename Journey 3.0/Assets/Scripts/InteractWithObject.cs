using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System.Linq;

public class InteractWithObject : MonoBehaviour
{
    private Animator _animator;
    private ObjectDetection _objectDetection;

    private InputSetUp _inputSetUp;

    [SerializeField] private bool _interacting;

    [SerializeField] private bool _nearRune;

    [SerializeField] private bool _inPlacementArea;


    public bool NearRune
    {
        get => _nearRune;
    }

    [SerializeField] private Transform handPosition;

    private PlayerMovement _movement;


    [SerializeField] private float _turnSpeed;


    private IInteractible _interactingObj;

    private IRune _rune;

    private Coroutine _coroutine;

    public Transform _chestHeight;

    [SerializeField] private bool cooldown;

    public AudioSource _source;

    public AudioClip clip;

    [SerializeField] private IPlaceableArea _plankPlacementArea;

    public IPlaceableArea PlankPlacement
    {
        get => _plankPlacementArea;
        set => _plankPlacementArea = value;
    }


    void Start()
    {
        _inputSetUp = GetComponent<InputSetUp>();
        _objectDetection = GetComponent<ObjectDetection>();
        _movement = GetComponent<PlayerMovement>();
        _coroutine = null;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _nearRune = CheckNearRune();

        if (_inputSetUp.Controls.PlayerFreeMovement.Interact.triggered && !cooldown)
        {
            if (_nearRune && _objectDetection.Items.Count > 1)
            {
                GameObject rune = _rune.getGameObject();
                GameObject interactible;

                if (_interacting)
                {
                    if (rune.GetComponent<HoldInteractipleOnRune>() != null &&
                        rune.GetComponent<HoldInteractipleOnRune>().ItemOnRuneBool)
                    {
                        return;
                    }

                    cooldown = true;
                    _movement.ControllerVeclocity = Vector3.zero;
                    _movement.enabled = false;

                    interactible = _interactingObj.getGameObject();

                    GameObject[] temp = new GameObject[] {rune, interactible};
                    _coroutine = StartCoroutine(UseRune(temp));
                }
                else
                {
                    if (rune.GetComponent<HoldInteractipleOnRune>() != null)
                    {
                        if (!rune.GetComponent<HoldInteractipleOnRune>().ItemOnRuneBool)
                        {
                            return;
                        }

                        cooldown = true;
                        _movement.ControllerVeclocity = Vector3.zero;
                        _movement.enabled = false;

                        interactible = rune.GetComponent<HoldInteractipleOnRune>().ItemOnRune;
                        _interactingObj = rune.GetComponent<HoldInteractipleOnRune>().ItemOnRune
                            .GetComponent<IInteractible>();
                        GameObject[] temp = new GameObject[] {rune, interactible};

                        _coroutine = StartCoroutine(UseRune(temp));
                    }
                    else
                    {
                        cooldown = true;
                        _movement.ControllerVeclocity = Vector3.zero;
                        _movement.enabled = false;

                        interactible = ReturnCloserObject();

                        GameObject[] temp = new GameObject[] {rune, interactible};
                        _coroutine = StartCoroutine(UseRune(temp));
                    }
                }
            }
            else if (!_interacting && !_nearRune && _objectDetection.Items.Count > 0 && !_inPlacementArea
            ) // pickup items in general
            {
                GameObject obj = ReturnCloserObject();
                _interactingObj = obj.GetComponent<IInteractible>();

                var type = _interactingObj.GetType();

                if (type == typeof(PushObject))
                {
                    cooldown = true;
                    _interacting = true;
                    _movement.ControllerVeclocity = Vector3.zero;
                    _movement.enabled = false;

                    _coroutine = StartCoroutine(TurnToPush(obj));
                }
                else if (type == typeof(PickUpObject))
                {
                    if (_plankPlacementArea != null && _plankPlacementArea.GetPlank() != null &&
                        _plankPlacementArea.GetPlank() != obj)
                    {
                        cooldown = true;
                        _interacting = true;
                        _movement.ControllerVeclocity = Vector3.zero;
                        _movement.enabled = false;

                        _coroutine = StartCoroutine(TurnToGrab(obj));
                    }
                    else if (_plankPlacementArea != null && _plankPlacementArea.GetPlank() != null &&
                             _plankPlacementArea.GetPlank() == obj)
                    {
                        return;
                    }
                    else
                    {
                        cooldown = true;
                        _interacting = true;
                        _movement.ControllerVeclocity = Vector3.zero;
                        _movement.enabled = false;

                        _coroutine = StartCoroutine(TurnToGrab(obj));
                    }
                }
            }
            else if (!_interacting && !_nearRune && _objectDetection.Items.Count > 0 && _inPlacementArea
            ) // pickup of item whilst in the placement zone (bridge)
            {
                GameObject obj = ReturnCloserObject();

                if (_plankPlacementArea.GetPlank() == null && obj.GetComponent<PickUpObject>().PlacedDown)
                {
                    print("in placement zone but returned here because of plank being placed down");
                    return;
                }

                print("plank specific pickup whilst in zone");
                cooldown = true;
                _interacting = true;
                _movement.ControllerVeclocity = Vector3.zero;
                _movement.enabled = false;


                _interactingObj = obj.GetComponent<IInteractible>();
                _coroutine = StartCoroutine(TurnToGrab(obj));

                if (obj == _plankPlacementArea.GetPlank())
                {
                    _plankPlacementArea.GetPlank().GetComponent<PickUpObject>().PlacedDown = false;
                    _plankPlacementArea.SetPlank(null);
                    _plankPlacementArea.SetPlankPlacedDown(false);
                }
            }
            else if (_interacting && !_nearRune && !_inPlacementArea) //Drop item you are currently holding
            {
                StopInteracting();
            }
            else if (_interacting && !_nearRune && _inPlacementArea) //Place plank
            {
                print("Plank placement while in zone");
                SquishObject squishObject = _interactingObj.getGameObject().GetComponent<SquishObject>();

                if (squishObject != null && squishObject.Squished && !_plankPlacementArea.GetPlankPlacedDown())
                {
                    StartCoroutine(PlacePlank(_interactingObj.getGameObject()));
                }
            }
        }
    }

    private float ReturnAngleToObj(Vector3 objPosition)
    {
        Vector3 toObject = objPosition - _chestHeight.position;
        Vector3 toObjectStraight =
            new Vector3(objPosition.x, _chestHeight.position.y, objPosition.z) - _chestHeight.position;

        return Vector3.Angle(toObject, toObjectStraight);
    }

    public void StopInteracting()
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
                    float thisDistance =
                        Vector3.Distance(transform.position, _objectDetection.Items[i].transform.position);

                    if (thisDistance < closestDistance)
                    {
                        closestDistance = thisDistance;
                        closestObj = _objectDetection.Items[i];
                    }
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

        _targetRotation.eulerAngles =
            new Vector3(transform.rotation.x, _targetRotation.eulerAngles.y, transform.rotation.z);


        while (Quaternion.Angle(transform.rotation, _targetRotation) > 10f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _turnSpeed);

            yield return new WaitForEndOfFrame();
        }

        float angleFromForward = ReturnAngleToObj(interactible.transform.position);

        if (angleFromForward > 10f && interactible.transform.position.y < _chestHeight.position.y)
        {
            gameObject.SendMessage("PickUpLow");
        }
        else
        {
            gameObject.SendMessage("PickUpHigh");
        }

        float animationTime = _movement.ReturnCurrentClipLength() / 2f;

        yield return new WaitForSeconds(animationTime);

        _source.PlayOneShot(clip);
        _interactingObj.StartInteraction(handPosition);

        if (_interactingObj.getGameObject().GetComponent<SquishObject>())
        {
            var placeableAreas = FindObjectsOfType<MonoBehaviour>().OfType<IPlaceableArea>();

            foreach (IPlaceableArea placeableArea in placeableAreas)
            {
                if (placeableArea.GetPlank() == _interactingObj.getGameObject())
                {
                    placeableArea.SetPlankPlacedDown(false);
                    placeableArea.SetPlank(null);
                }
            }
        }

        yield return new WaitForEndOfFrame();


        yield return new WaitUntil(() => !_animator.IsInTransition(0));

        _movement.enabled = true;
        cooldown = false;
    }


    IEnumerator PlacePlank(GameObject interactible)
    {
        _movement.enabled = false;


        Quaternion _targetRotation =
            Quaternion.LookRotation(_plankPlacementArea.GetCenterObject().transform.right, Vector3.up);

        _targetRotation.eulerAngles =
            new Vector3(transform.rotation.x, _targetRotation.eulerAngles.y, transform.rotation.z);


        while (Quaternion.Angle(transform.rotation, _targetRotation) > 10f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _turnSpeed);

            yield return new WaitForEndOfFrame();
        }

        _source.PlayOneShot(clip);

        print(interactible);

        interactible.GetComponent<PickUpObject>().PlacedDown = true;

        _plankPlacementArea.SetPlank(interactible);

        StopInteracting();

        _plankPlacementArea.SetPlankPlacedDown(true);

        /*if (_plankPlacementArea.AdjustPositionBool())
        {
            PositionAdjustment positionAdjustment =
                _plankPlacementArea.GetCenterObject().GetComponent<PositionAdjustment>();
            PositionAdjustmentDouble positionAdjustmentDouble =
                _plankPlacementArea.GetCenterObject().GetComponent<PositionAdjustmentDouble>();

            if (positionAdjustment != null)
            {
                positionAdjustment.PlankPlacedDown = true;
            }

            if (positionAdjustmentDouble != null)
            {
                positionAdjustmentDouble.PlankPlacedDown = true;
            }
        }*/

        interactible.GetComponent<Rigidbody>().isKinematic = true;
        interactible.GetComponent<Collider>().isTrigger = true;

        _plankPlacementArea.GetPlank().transform.position = _plankPlacementArea.GetCenterObject().transform.position;
        _plankPlacementArea.GetPlank().transform.rotation = _plankPlacementArea.GetCenterObject().transform.rotation;

        _plankPlacementArea.GetPlank().transform.rotation = _plankPlacementArea.GetPlank().transform.rotation *
                                                            Quaternion.AngleAxis(90f,
                                                                _plankPlacementArea.GetPlank().transform.up);


        yield return new WaitForEndOfFrame();


        yield return new WaitUntil(() => !_animator.IsInTransition(0));

        _movement.enabled = true;
        cooldown = false;
    }


    IEnumerator UseRune(GameObject[] runeAndInteractible)
    {
        bool adjustCoolDown = false;

        GameObject rune = runeAndInteractible[0];
        GameObject interactible = runeAndInteractible[1];

        IInteractible _interactible = interactible.GetComponent<IInteractible>();

        if (interactible.GetComponent<GravityCheck>() != null && _interactible.isActive())
            _interactible.StopInteraction();

        if (rune.GetComponent<GrowObject>() != null)
        {
            ChangeSize _change = interactible.GetComponent<ChangeSize>();
            _change.StartChangeSize();
        }

        if (rune.GetComponent<HoldInteractipleOnRune>() != null)
        {
            HoldInteractipleOnRune holdInteractipleOnRune = rune.GetComponent<HoldInteractipleOnRune>();

            if (!holdInteractipleOnRune.ItemOnRuneBool)
            {
                interactible.transform.rotation = Quaternion.Euler(0, interactible.transform.rotation.eulerAngles.y, 0);
                interactible.transform.position = holdInteractipleOnRune.ObjectPlaceArea.position;

                StopInteracting();

                Collider col = interactible.GetComponent<Collider>();
                Renderer rend = interactible.GetComponentInChildren<Renderer>();


                yield return new WaitForEndOfFrame();

                float ySize = rend.bounds.size.y;

                col.isTrigger = true;

                interactible.transform.position = interactible.transform.position + new Vector3(0, ySize / 2f, 0);

                rune.GetComponent<OpenForPlayer>().ItemPresentHeightOffset = ySize;

                interactible.GetComponent<Rigidbody>().isKinematic = true;

                holdInteractipleOnRune.ItemOnRune = interactible;

                holdInteractipleOnRune.ItemOnRuneBool = true;
            }
            else
            {
                holdInteractipleOnRune.ItemOnRune = null;
                holdInteractipleOnRune.ItemOnRuneBool = false;
                rune.GetComponent<OpenForPlayer>().ItemPresentHeightOffset = 0f;
                StartCoroutine(TurnToGrab(interactible));
                _interacting = true;
                adjustCoolDown = true;
            }
        }

        yield return new WaitForEndOfFrame();


        if (!adjustCoolDown)
        {
            _movement.enabled = true;
            cooldown = false;
        }
    }

    IEnumerator TurnToPush(GameObject interactible)
    {
        Quaternion _targetRotation =
            Quaternion.LookRotation(interactible.transform.position - transform.position, Vector3.up);

        _targetRotation.eulerAngles =
            new Vector3(transform.rotation.x, _targetRotation.eulerAngles.y, transform.rotation.z);

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
        cooldown = false;
    }


    public void ChangeInPlacementBool(bool newValue)
    {
        _inPlacementArea = newValue;
    }

    public void SetPlacementArea(IPlaceableArea newPlacementArea)
    {
        _plankPlacementArea = newPlacementArea;
    }
}