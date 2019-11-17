using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushObject : MonoBehaviour
{
    private Transform cam;


    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _turnSpeed = 10f;
    [SerializeField] private float _gravity = 20f;
    [SerializeField] private float _jumpSpeed = 10f;

    [SerializeField] private bool _jump;


    private Vector2 _input;
    private float _angle;

    private Quaternion _targetRotation;
    private CharacterController _controller;

    private Vector3 _movementDirection;

    public bool grounded;

    private float _timeDelta;

    private InputSetUp _inputSetUp;

    void Start()
    {
        if (cam == null)
            cam = Camera.main.transform;

        _controller = GetComponent<CharacterController>();
        _inputSetUp = GetComponent<InputSetUp>();
    }

    // Update is called once per frame
    void Update()
    {
        _input = _inputSetUp.LeftStick;

        grounded = _controller.isGrounded;
        _timeDelta = Time.deltaTime;

        if (_controller.isGrounded)
        {
            SetMove();

            if (_inputSetUp.Controls.PlayerFreeMovement.Jump.triggered && _jump)
            {
            }
        }


        ApplyGravity();

        _controller.Move(_movementDirection * _timeDelta);
    }

    private void ApplyGravity()
    {
        _movementDirection.y -= _gravity * _timeDelta;
    }

    private void SetMove()
    {
        Vector3 right = cam.right;
        Vector3 forward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 movement = (right * _input.x) + (forward * _input.y);

        if (Vector3.Angle(transform.forward, movement) > 120f)
        {
            _movementDirection = -transform.forward * _input.magnitude;
            _movementDirection *= _speed;
        }
        else if (Vector3.Angle(transform.forward, movement) < 60f)
        {
            _movementDirection = transform.forward * _input.magnitude;
            _movementDirection *= _speed;
        }
    }
}