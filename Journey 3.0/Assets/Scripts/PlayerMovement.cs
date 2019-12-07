using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. Movement based of camera
/// 2. stop and face dircetion when input is absent
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private Transform cam;

    [SerializeField] private Animator _anim;


    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _pushSpeed = 2f;

    

    [SerializeField] private float _turnSpeed = 10f;
    [SerializeField] private float _gravity = 20f;
    [SerializeField] private float _jumpSpeed = 10f;

    [SerializeField] private bool _jump;

    [SerializeField] private float _minWalkSpeed = 1f;
    [SerializeField] private float _maxWalkSpeed = 20f;


    private Vector2 _input;
    private float _angle;

    private Quaternion _targetRotation;
    private CharacterController _controller;

    private Vector3 _movementDirection;

    public bool carryingObject;

    private float _originalRange;

    public Vector3 MovementDirection
    {
        get => _movementDirection;
    }
    
    public Vector3 ControllerVeclocity
    {
        get => _controller.velocity;
        set => _controller.Move(value);
    }

    public bool grounded;

    [SerializeField] private bool _pushing;

    public bool Pushing
    {
        get => _pushing;
        set => _pushing = value;
    }

    private float _timeDelta;

    private InputSetUp _inputSetUp;


    public RayInfo info;

    public LayerMask mask;

    
    void Start()
    {
        if (cam == null)
            cam = Camera.main.transform;

        _controller = GetComponent<CharacterController>();
        _inputSetUp = GetComponent<InputSetUp>();

        _originalRange = _controller.radius;
    }

    private void Update()
    {
        if (carryingObject)
        {
            _controller.radius = _originalRange * 1.5f;
        }
        else
        {
            _controller.radius = _originalRange;
        }

        _input = _inputSetUp.LeftStick;

        grounded = _controller.isGrounded;
        _timeDelta = Time.deltaTime;

        CalculateDirection();

        if (_controller.isGrounded)
        {
            if (Mathf.Abs(_input.magnitude) > 0.05f && !_pushing)
            {
                Rotate();
            }

            SetMove();

            if (_inputSetUp.Controls.PlayerFreeMovement.Jump.triggered && _jump && ! _pushing)
            {
                Jump();
            }
        }


        ApplyGravity();

        _controller.Move(_movementDirection * _timeDelta);

        SetAnimation();
    }

    private void SetAnimation()
    {
        _anim.SetFloat("velocity", _controller.velocity.magnitude);

        float walkSpeed = Map(_controller.velocity.magnitude, 0f, _speed, _minWalkSpeed, _maxWalkSpeed);
        
        _anim.SetFloat("walkSpeed", walkSpeed);
        
        _anim.SetBool("pushing", _pushing);
    }

    void PickUpLow()
    {
        _anim.SetTrigger("pickUp");
    }

    void PickUpHigh()
    {
        _anim.SetTrigger("grab");
    }

    /// <summary>
    /// Calculates the direction of movement
    /// </summary>
    void CalculateDirection()
    {
        _angle = Mathf.Atan2(_input.x, _input.y);
        _angle = Mathf.Rad2Deg * _angle;
        _angle += cam.eulerAngles.y;
    }

    /// <summary>
    /// Rotate towards the target direction
    /// </summary>
    void Rotate()
    {
        _targetRotation = Quaternion.Euler(0, _angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _turnSpeed * _timeDelta);
    }

    /// <summary>
    /// Sets the moveDirections along forward axis by the speed variable
    /// </summary>
    void SetMove()
    {
        float angleToStraight = Quaternion.Angle(_targetRotation, transform.rotation);

        float adjustedSpeed = Map(angleToStraight, 180, 0, 0f, _speed);
        
        Vector3 right = cam.right;
        Vector3 forward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 movement = (right * _input.x) + (forward * _input.y);

        if (!_pushing)
        {
            _movementDirection = movement;
            _movementDirection *= adjustedSpeed;
            //_movementDirection *= _speed;
        }
        else
        {
            float localPushDirection;
            if (Vector3.Angle(transform.forward, movement) > 120f)
            {
                _movementDirection = -transform.forward;
                _movementDirection *= _pushSpeed * _input.magnitude;
                localPushDirection = -1f * _input.magnitude;
            }
            else if (Vector3.Angle(transform.forward, movement) < 60f)
            {
                Debug.DrawRay(info.position, transform.forward * info.distance);
                
                Ray ray = new Ray(info.position, transform.forward);
                
                if (!Physics.SphereCast(ray, 1f, info.distance, mask))
                {
                    _movementDirection = transform.forward;
                    _movementDirection *= _pushSpeed * _input.magnitude;
                    localPushDirection = 1f * _input.magnitude;
                }
                else
                {
                    _movementDirection = forward * 0f;
                    localPushDirection = 0;
                }
            }
            else
            {

                localPushDirection = 0f;
            }
            _anim.SetFloat("pushDirection", localPushDirection);
        }
    }

    /// <summary>
    /// Apply gravity to the moveDirections y axis
    /// </summary>
    void ApplyGravity()
    {
        _movementDirection.y -= _gravity * _timeDelta;
    }


    /// <summary>
    /// Set the move directions y to the jump speed
    /// this has to be called after set move, otherwise the y gets reset
    /// </summary>
    void Jump()
    {
        _movementDirection.y = _jumpSpeed;
    }
    
    public float Map(float a, float b, float c, float d, float e)
    {
        
        float cb = c - b;
        float de = e - d;
        float howFar = (a - b) / cb;
        return d + howFar * de;
        
        //float a = value you want mapped t
    }


    public float ReturnCurrentClipLength()
    {
        return _anim.GetCurrentAnimatorClipInfo(0).Length;
    }


    public void DisableThis()
    {
        _anim.SetFloat("velocity", 0f);
        this.enabled = false;
    }
}