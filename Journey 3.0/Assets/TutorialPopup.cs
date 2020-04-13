using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopup : MonoBehaviour
{
    [SerializeField] private GameObject[] _interactionObjectPress;

    [SerializeField] private GameObject[] _interactionObjectHold;

    [SerializeField]private InputSetUp _inputSetUp;

    [SerializeField]private ObjectDetection _objectDetection;
    [SerializeField] private GameObject _player;

    private GameObject objectToDisplayTutorialFor;
    
    // Start is called before the first frame update
    void Start()
    {
        //_player = API.GlobalReferences.PlayerRef;

        if (_player == null)
        {
            _player = GameObject.Find("Player");
        }

        _inputSetUp = _player.GetComponent<InputSetUp>();

        _objectDetection = _player.GetComponent<ObjectDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_objectDetection.Items.Count > 0)
        {
            GameObject tempObjectToDisplayTutorialFor = FindRelevantObject(_objectDetection.Items.ToArray());
            
            if (objectToDisplayTutorialFor == null || objectToDisplayTutorialFor != tempObjectToDisplayTutorialFor)
            {
                if (objectToDisplayTutorialFor.GetComponent<PushObject>() != null)
                {
                    if (_inputSetUp.lastInput == "Gamepad")
                    {
                        _interactionObjectHold[0].GetComponent<Animator>().Play("HoldButton", 0, 0f);
                    }
                    else
                    {
                        _interactionObjectHold[1].GetComponent<Animator>().Play("HoldButton", 0, 0f);
                    }
                }
                else
                {
                    if (_inputSetUp.lastInput == "Gamepad")
                    {
                        _interactionObjectPress[0].GetComponent<Animator>().Play("PressButton", 0, 0f);
                    }
                    else
                    {
                        _interactionObjectPress[1].GetComponent<Animator>().Play("PressButton", 0, 0f);
                    }
                }

                objectToDisplayTutorialFor = tempObjectToDisplayTutorialFor;
            }


            
        }
    }

    GameObject FindRelevantObject(GameObject[] itemsToCheck)
    {
        if (itemsToCheck.Length == 1)
        {
            return itemsToCheck[0];
        }
        else
        {
            GameObject relevantObject = CheckClosestItem(itemsToCheck);
            return relevantObject;
        }

    }

    GameObject CheckClosestItem(GameObject[] objects)
    {
        GameObject closestObj = objects[0];

        float closestDistance = float.MaxValue;

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].GetComponent<IRune>() == null)
            {
                float thisDistance =
                    Vector3.Distance(_player.transform.position, objects[i].transform.position);

                if (thisDistance < closestDistance)
                {
                    closestDistance = thisDistance;
                    closestObj = objects[i];
                }
            }
        }

        return closestObj;
    }
}
