using UnityEngine;


public class PushObject : MonoBehaviour, IInteractible
{
    private GameObject _player;

    private Rigidbody _rb;

    private bool _pushing;

    private Vector3 _position;

    private PlayerMovement _movement;

    private float _distanceToPushingObject;

    [SerializeField] private float _minDistance = 1.47f;

    private InteractWithObject _interactWithObject;

    private InputSetUp _inputSetUp;

    private Transform playerChest;

    public LayerMask mask;

    private Collider _col;

    private AudioSource _source;

    [SerializeField] private float _positionLerpSpeed = 1f;

    [SerializeField] PushCollisionDetection _pushCollisionDetection;

    private float oldRadius;

    private CharacterController characterController;


    public float _audioDistance = 0.02f;

    void OnEnable()
    {
        _interactWithObject = GetComponent<InteractWithObject>();
        _inputSetUp = GetComponent<InputSetUp>();

        _pushCollisionDetection = GetComponentInChildren<PushCollisionDetection>();

        string[] maskTargets = new string[] {"Ground", "Wall", "Default"};
        mask = LayerMask.GetMask(maskTargets);

        _col = GetComponent<Collider>();
        _source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (_pushing)
        {
            if (_inputSetUp.ValueInteractDown >= 0.9f)
            {
                _position = _player.transform.TransformPoint(Vector3.forward * (_distanceToPushingObject));


                _position.y = transform.position.y;


                float internalDistance = _col.bounds.extents.x;

                Ray ray = new Ray(playerChest.position + (_player.transform.up / 2f), _player.transform.forward);

                RaycastHit hit;


                _movement.info.distance = _distanceToPushingObject + internalDistance;
                _movement.info.position = ray.origin;


                if (!Physics.SphereCast(ray, 0.2f, out hit, _distanceToPushingObject + internalDistance, mask))
                {
                    if (characterController.velocity.magnitude > 0.05f)
                    {
                        _rb.MovePosition(_position);
                    }
                    else
                    {
                        _rb.MovePosition(Vector3.Lerp(transform.position, _position, _positionLerpSpeed));
                    }



                    if (_movement.PushingBoulder())
                    {
                        if (!_source.isPlaying)
                            _source.Play();
                    }
                    else
                    {
                        _source.Stop();
                    }
                }
            }
            else
            {
                _source.Stop();
                _interactWithObject.StopInteracting();
            }
        }
    }


    public GameObject getGameObject()
    {
        return gameObject;
    }

    public bool isActive()
    {
        return _pushing;
    }

    public void StartInteraction(Transform parent)
    {
        gameObject.layer = 18;
        _pushing = true;
        _player = parent.root.gameObject;
        CharacterController cc = _player.GetComponent<CharacterController>();

        oldRadius = cc.radius;

        cc.radius = oldRadius * 0.35f;
        characterController = cc;

        SetUpPushing();
    }

    private void SetUpPushing()
    {
        GetComponentInChildren<AdjustBounceRotation>().AsignPushObject(this);
        _movement = _player.GetComponent<PlayerMovement>();
        _movement.ControllerVeclocity = Vector3.zero;
        _movement._pushCollisionDetection = this._pushCollisionDetection;
        _movement.Pushing = true;
        playerChest = _player.GetComponent<InteractWithObject>()._chestHeight;
        _interactWithObject = _player.GetComponent<InteractWithObject>();
        _inputSetUp = _player.GetComponent<InputSetUp>();


        Vector3 sameHeightPos = new Vector3(playerChest.transform.position.x, transform.position.y,
            _player.transform.position.z);

        _distanceToPushingObject = _minDistance;
        //_distanceToPushingObject = Vector3.Distance(sameHeightPos, transform.position);


        if (_distanceToPushingObject < _minDistance)
            _distanceToPushingObject = _minDistance;

        if (_rb == null)
            _rb = GetComponent<Rigidbody>();

        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void StopInteraction()
    {
        _pushing = false;
        _movement.Pushing = false;

        print("Push Finished");
        CharacterController cc = _player.GetComponent<CharacterController>();
        cc.radius = oldRadius;

        _movement._pushCollisionDetection = null;

        gameObject.layer = 0;

        _rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnDrawGizmos()
    {
        if (_pushing)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_position, 1f);
        }
    }

    public void GetRayInfo()
    {
    }
}

public struct RayInfo
{
    public Vector3 position;
    public float distance;
}