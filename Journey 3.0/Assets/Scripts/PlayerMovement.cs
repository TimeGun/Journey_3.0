using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

/// <summary>
/// 1. Movement based of camera
/// 2. stop and face dircetion when input is absent
/// </summary>
///
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    private Transform cam;
    
    
    [SerializeField] private float _velocity = 5f;
    [SerializeField] private float _turnSpeed = 10f;
    [SerializeField] private float _height = 0.5f;
    [SerializeField] private float _width = 0.5f;
    [SerializeField] private float _heightPadding = 0.05f;
    [SerializeField] private float _maximumAngle = 0.05f;
    [SerializeField] private bool _debug;

    [SerializeField] private LayerMask ground;
    
    

    private Vector2 input;
    private float angle;
    private float groundAngle;

    private Quaternion targetRotation;


    private Vector3 forward;
    private RaycastHit hitInfo;
    [SerializeField]private bool grounded;

    private Rigidbody _rb; 
    
    void Start()
    {
        if (cam == null)
            cam = Camera.main.transform;

        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        GetInput();
        CalculateDirection();
        CalculateForward();
        CalculateGroundAngle();
        CheckGround();
        ApplyGravity();
        DrawDebugLines();

        if (Mathf.Abs(input.x) < 0.1f && Mathf.Abs(input.y) < 0.1f)
        {
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
            return;
        }

        Rotate();
        Move();
    }

    /// <summary>
    ///  Input based of new Input system
    /// </summary>
    void GetInput()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
    }
    
    /// <summary>
    /// Calculates the direction of movement
    /// </summary>
    void CalculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }
    
    /// <summary>
    /// Rotate towards the target direction
    /// </summary>
    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
    }
    
    /// <summary>
    /// Move along forward axis
    /// </summary>
    void Move()
    {
        if (groundAngle >= _maximumAngle) return;
        _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        _rb.velocity += forward * (_velocity * Time.deltaTime);
    }
    
    /// <summary>
    /// If the player is not grounded, forward will be equal to transform forward
    /// Use cross product to determine the new forward vector
    /// </summary>
    void CalculateForward()
    {
        if (!grounded)
        {
            forward = transform.forward;
            return;
        }

        forward = Vector3.Cross(transform.right, hitInfo.normal);
    }

    /// <summary>
    /// Vector 3 angle between the ground normal and the transform forward to determine the slop of the ground
    /// </summary>
    void CalculateGroundAngle()
    {
        if (!grounded)
        {
            groundAngle = 90f;
            return;
        }

        groundAngle = Vector3.Angle(hitInfo.normal, transform.forward);
    }

    /// <summary>
    /// Use a raycast to check if the player is on ground
    /// </summary>
    void CheckGround()
    {
        Vector3 infront = transform.TransformPoint(0, 0, _width);
        
        if(Physics.Raycast(infront, -Vector3.up, out hitInfo, _height + _heightPadding, ground))
        {
            //if (Vector3.Distance(transform.position, hitInfo.point) < _height)
            //{
            //    transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * _height, 5f * Time.deltaTime);
            //}

            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
    
    /// <summary>
    /// Apply gravity when the player isn't grounded 
    /// </summary>
    void ApplyGravity()
    {
        if (!grounded)
        {
            _rb.useGravity = true;
        }
        else
        {
            _rb.useGravity = false;
        }
    }

    /// <summary>
    /// For debugging the player
    /// </summary>
    void DrawDebugLines()
    {
        if (!_debug)
        {
            return;
        }
        Vector3 infront = transform.TransformPoint(0, 0, _width);
        Debug.DrawLine(transform.position, transform.position + forward * _height * 2f, Color.blue);
        Debug.DrawLine(infront, infront- Vector3.up * (_height + _heightPadding) , Color.green);
    }
}
