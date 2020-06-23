using UnityEngine;

public class RaiseTorchCollider : MonoBehaviour
{
    [SerializeField] private ListOfIKSettings _listOfIkSettings;

    private ObjectDetection _objectDetection;

    private TorchOnOff _torchOnOff;

    private float timer = 0;

    public float MaxRaiseTime = 2f;

    private bool raise = true;

    private bool changedIk = false;

    [SerializeField] private BoxCollider _col;

    private void Start()
    {
        _col = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            raise = true;
            changedIk = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (_objectDetection == null)
                _objectDetection = other.GetComponent<ObjectDetection>();


            if (_objectDetection.carryingObject != null &&
                _objectDetection.carryingObject.GetComponent<TorchOnOff>() != null)
            {
                if (raise)
                {
                    if (!changedIk)
                    {
                        RightArmIK.Instance.SwitchIKSettings(_listOfIkSettings._premadeIKSettings[0],
                            _listOfIkSettings._premadeIKSettings[1]);
                        changedIk = true;
                    }

                    if (timer > MaxRaiseTime)
                    {
                        raise = false;
                    }
                    else
                    {
                        timer += Time.deltaTime;
                        print(timer);
                    }
                }
                else
                {
                    if (changedIk)
                    {
                        changedIk = false;
                        RightArmIK.Instance.SwitchIKSettings(_listOfIkSettings._premadeIKSettings[1],
                            _listOfIkSettings._premadeIKSettings[0]);
                        timer = 0;
                    }
                }
            }
            else
            {
                raise = true;
                changedIk = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (_objectDetection.carryingObject != null &&
                _objectDetection.carryingObject.GetComponent<TorchOnOff>() != null)
            {
                RightArmIK.Instance.SwitchIKSettings(_listOfIkSettings._premadeIKSettings[1],
                    _listOfIkSettings._premadeIKSettings[0]);
            }

            raise = false;
            timer = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.TransformPoint(_col.center), Vector3.Scale(_col.size, transform.localScale));
    }

    private void OnDisable()
    {
        if (_objectDetection != null && _objectDetection.carryingObject != null &&
            _objectDetection.carryingObject.GetComponent<TorchOnOff>() != null)
        {
            RightArmIK.Instance.SwitchIKSettings(_listOfIkSettings._premadeIKSettings[1],
                _listOfIkSettings._premadeIKSettings[0]);
        }
        
        raise = false;
        timer = 0;
    }
}