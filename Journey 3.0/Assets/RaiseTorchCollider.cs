using System;
using System.Collections;
using System.Collections.Generic;
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
    

    private void OnTriggerEnter(Collider other)
    {
        raise = true;
        changedIk = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (_objectDetection == null)
                _objectDetection = other.GetComponent<ObjectDetection>();
            
            
            if (_objectDetection.carryingObject != null && _objectDetection.carryingObject.GetComponent<TorchOnOff>() != null)
            {
                if (raise)
                {
                    if (!changedIk)
                    {
                        RightArmIK.Instance.SetIkTargetAndHint(_listOfIkSettings._premadeIKSettings[1], _listOfIkSettings._premadeIKSettings[1].lerpPercentage, true);
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
                        RightArmIK.Instance.SetIkTargetAndHint(_listOfIkSettings._premadeIKSettings[0], _listOfIkSettings._premadeIKSettings[0].lerpPercentage, false);
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

        if (_objectDetection.carryingObject != null &&
            _objectDetection.carryingObject.GetComponent<TorchOnOff>() != null)
        {
            RightArmIK.Instance.SetIkTargetAndHint(_listOfIkSettings._premadeIKSettings[0], _listOfIkSettings._premadeIKSettings[0].lerpPercentage, false);
        }

        raise = false;
        timer = 0;
    }
}
