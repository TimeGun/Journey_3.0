using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using VSCodeEditor;
using Object = System.Object;

public class PushObject : MonoBehaviour, IInteractible
{
    private GameObject _player;

    private Rigidbody _rb;

    private bool _pushing;

    private PlayerMovement _movement;

    private CharacterController _characterController;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (_pushing)
        {
            Vector3 force = _movement.MovementDirection;
            //_rb.AddForce(force, ForceMode.VelocityChange);
            //_rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _movement.ControllerVeclocityMagnitude);

            _characterController.Move(force * Time.deltaTime);
            
            print(_movement.ControllerVeclocityMagnitude);
        }

    }


    public void StartInteraction(Transform parent)
    {
        _pushing = true;
        _player = parent.root.gameObject;
        _movement = _player.GetComponent<PlayerMovement>();
        _movement.Pushing = true;
        if(_rb == null)
            _rb = GetComponent<Rigidbody>();

        _characterController = GetComponent<CharacterController>();

        _characterController.enabled = true;

        _rb.isKinematic = false;
    }

    public void StopInteraction()
    {
        _characterController.enabled = false;

        _pushing = false;
        _movement.Pushing = false;
        _rb.isKinematic = true;
    }
}
