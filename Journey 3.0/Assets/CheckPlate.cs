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

    [SerializeField] private UnityEvent openDoor;

    [SerializeField] private Animator _gateAnim;
    [SerializeField] private Animator _platformAnim;
    
    private void OnTriggerEnter(Collider other)
    {
        PushObject _pushable = other.GetComponent<PushObject>();

        PlayerMovement _movement = other.GetComponent<PlayerMovement>();

        
        if (_pushable != null)
        {
            _boulderPresent = true;
            if (!opened)
            {
                _pushable.transform.parent = transform.root;
                Destroy(_pushable);
                
                openDoor.Invoke();
                opened = true;
            }
        }
        
        if (_movement != null)
        {
            player = true;
        }
    }

    private void Update()
    {
        _gateAnim.SetBool("playerWeight", player);
        _platformAnim.SetBool("playerWeight", player);
    }

    private void OnTriggerExit(Collider other)
    {
        PushObject _pushable = other.GetComponent<PushObject>();

        PlayerMovement _movement = other.GetComponent<PlayerMovement>();
        

        if (_pushable != null)
        {
            _boulderPresent = false;
        }

        if (_movement != null)
        {
            player = false;
        }
    }
}
