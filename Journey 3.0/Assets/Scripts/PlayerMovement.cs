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
[RequireComponent(typeof(CharacterController))]
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


    private RaycastHit hitInfo;
    [SerializeField]private bool grounded;

    private CharacterController _controller; 
    
    void Start()
    {
        if (cam == null)
            cam = Camera.main.transform;

        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {

        GetInput();
        CalculateDirection();
        
        if (Mathf.Abs(input.x) < 0.1f && Mathf.Abs(input.y) < 0.1f) return;
        
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
        _controller.Move(transform.forward * (_velocity * Time.deltaTime));
    }
    
}
