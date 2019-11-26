using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using VSCodeEditor;
using Object = System.Object;

public class PushObject : MonoBehaviour, IInteractible
{
    private GameObject _player;

    private Rigidbody _rb;

    private bool _pushing;

    private Vector3 _position;

    private PlayerMovement _movement;
    
    private float _distanceToPushingObject;

    [SerializeField] private float _minDistance = 2.2f;

    private InteractWithObject _interactWithObject;

    private InputSetUp _inputSetUp;
    
    

    void Start()
    {
        _interactWithObject = GetComponent<InteractWithObject>();
        _inputSetUp = GetComponent<InputSetUp>();
    }

    void FixedUpdate()
    {
        if (_pushing)
        {
            if (_inputSetUp.ValueInteractDown >= 0.9f)
            {
                _position = _player.transform.TransformPoint(Vector3.forward * _distanceToPushingObject);

                _position.y = transform.position.y;

            
                _rb.MovePosition(Vector3.Lerp(transform.position, _position, Time.deltaTime * 25f));
            }
            else
            {
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
        _pushing = true;
        _player = parent.root.gameObject;


        SetUpPushing();
        
    }

    private void SetUpPushing()
    {
        _movement = _player.GetComponent<PlayerMovement>(); 
        _movement.ControllerVeclocity = Vector3.zero;
        _movement.Pushing = true;
        
        _interactWithObject = _player.GetComponent<InteractWithObject>();
        _inputSetUp = _player.GetComponent<InputSetUp>();
        
        _distanceToPushingObject = Vector3.Distance(_player.transform.position, transform.position);

        
        if (_distanceToPushingObject < _minDistance)
            _distanceToPushingObject = _minDistance;
        
        if (_rb == null)
            _rb = GetComponent<Rigidbody>();

        _rb.isKinematic = false;
    }

    public void StopInteraction()
    {
        _pushing = false;
        _movement.Pushing = false;
        _rb.isKinematic = true;
    }

    private void OnDrawGizmos()
    {
        if (_pushing)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_position, 1f);
        }
    }
}