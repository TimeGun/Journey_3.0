using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckPlate : MonoBehaviour
{
    [SerializeField] private bool _boulderPresent;

    private bool opened = false;

    [SerializeField] private bool player;

    [SerializeField] private bool smallBoulder;

    [SerializeField] private UnityEvent openDoor;

    [SerializeField] private Animator _gateAnim;
    [SerializeField] private Animator _platformAnim;

    public Transform boulderParent;

    public ReturnBoulderPresent _ReturnBoulderPresent;

    [SerializeField] private AchievementSO achievementSo;

    private void OnTriggerEnter(Collider other)
    {
        ChangeSize _changeSize = other.GetComponent<ChangeSize>();

        PlayerMovement _movement = other.GetComponent<PlayerMovement>();


        if (_changeSize != null && _changeSize.Small && _changeSize.name == "Boulder")
        {
            smallBoulder = true;
        }

        if (_movement != null && !other.isTrigger)
        {
            player = true;
        }
    }

    private void Update()
    {
        if (player || smallBoulder)
        {
            if (_gateAnim.isActiveAndEnabled)
                _gateAnim.SetBool("playerWeight", true);


            if (_platformAnim.isActiveAndEnabled)
            {
                _platformAnim.SetBool("playerWeight", true);
            }
        }
        else
        {
            if (_gateAnim.isActiveAndEnabled)
                _gateAnim.SetBool("playerWeight", false);


            if (_platformAnim.isActiveAndEnabled)
            {
                _platformAnim.SetBool("playerWeight", false);
            }
        }


        if (_ReturnBoulderPresent.Boulder)
        {
            if (!opened)
            {
                _ReturnBoulderPresent.pushableObj.SetParent(boulderParent, true);
                _ReturnBoulderPresent.pushableObj.GetComponent<Rigidbody>().velocity = Vector3.zero;


                InteractibleGlow _glow = API.GlobalReferences.PlayerRef.GetComponent<InteractibleGlow>();

                Destroy(_glow);


                openDoor.Invoke();
                API.GlobalReferences.PlayerRef.GetComponent<InteractWithObject>().StopInteracting();

                opened = true;
                
                if (achievementSo != null)
                {
                    AchievementManager.instance.UnlockSteamAchievement(achievementSo);
                }

                Invoke("DestroyBoulder", Time.deltaTime);
            }
        }
    }

    void DestroyBoulder()
    {
        Destroy(_ReturnBoulderPresent.pushableObj.GetComponent<GravityCheck>());
        Destroy(_ReturnBoulderPresent.pushableObj.GetComponent<Rigidbody>());
        Destroy(_ReturnBoulderPresent.pushableObj.GetComponent<PushObject>());
        Destroy(_ReturnBoulderPresent.pushableObj.GetComponent<SquishObject>());
        API.GlobalReferences.PlayerRef.GetComponent<ObjectDetection>().Items
            .Remove(_ReturnBoulderPresent.pushableObj.gameObject);
        Invoke("EnableGlow", 1f);
    }

    void EnableGlow()
    {
        API.GlobalReferences.PlayerRef.AddComponent<InteractibleGlow>();
    }

    private void OnTriggerExit(Collider other)
    {
        ChangeSize _changeSize = other.GetComponent<ChangeSize>();

        PlayerMovement _movement = other.GetComponent<PlayerMovement>();

        if (_changeSize != null && _changeSize.Small && _changeSize.name == "Boulder")
        {
            smallBoulder = false;
        }


        if (_movement != null)
        {
            player = false;
        }
    }
}