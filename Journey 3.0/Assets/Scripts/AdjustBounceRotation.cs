using UnityEngine;

/// <summary>
/// Adjusts rotation on the collider that stops the player from climbing the boulder
/// </summary>
public class AdjustBounceRotation : MonoBehaviour
{
    private GameObject _player;

    [SerializeField] private ChangeSize _changeSize;

    private BoxCollider _collider;

    private PushObject _pushObject;

    private Vector3 originalColliderCenter;

    public float maxZAdditionValue;

    private float originalDistance;

    public float additionalDistanceBuffer = 0.1f;

    private float previousFrameDistance;
    
    private bool lerpZValue;
    
    void Start()
    {
        _player = GameObject.Find("Player");
        _collider = GetComponent<BoxCollider>();

        originalColliderCenter = _collider.center;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerAtThisHeight = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
        transform.rotation = Quaternion.LookRotation(playerAtThisHeight - transform.position, Vector3.up);
        
        if (!_changeSize.Small)
        {
            if (_pushObject != null)
            {
                _collider.isTrigger = false;
                if (_pushObject.isActive())
                {
                    originalDistance = 0f;
                    //Set the position to min value
                    _collider.center = originalColliderCenter;
                    lerpZValue = true;
                    previousFrameDistance = 0f;
                }
                else
                {
                    SetOriginalDistance();
                    float currentDistance = Vector3.Distance(transform.position, _player.transform.position);
                    
                    float extraDistance = currentDistance - (originalDistance + additionalDistanceBuffer);

                    if (extraDistance > previousFrameDistance)
                    {
                        previousFrameDistance = extraDistance;
                    }
                    
                    _collider.center = originalColliderCenter + new Vector3(0, 0, Mathf.Clamp(previousFrameDistance, 0f, maxZAdditionValue));
                }
            }
            else
            {
                _collider.isTrigger = false;
            }
        }
        else
        {
            _collider.isTrigger = true;
        }
    }

    private void SetOriginalDistance()
    {
        if (originalDistance <= 0f)
        {
            print("og distance set");
            originalDistance = Vector3.Distance(transform.position, _player.transform.position);
        }
    }

    public void AsignPushObject(PushObject value)
    {
        _pushObject = value;
    }
    
    public float Map(float a, float b, float c, float d, float e)
    {
        float cb = c - b;
        float de = e - d;
        float howFar = (a - b) / cb;
        return d + howFar * de;

        //float a = value you want mapped t
    }
}
