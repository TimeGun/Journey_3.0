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
    
    
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _turnSpeed = 10f;
    [SerializeField] private float _gravity = 20f;

    private Vector2 input;
    private float angle;

    private Quaternion targetRotation;


    private RaycastHit hitInfo;
    private CharacterController _controller;

    private Vector3 movementDirection;

    public bool grounded;
    
    void Start()
    {
        if (cam == null)
            cam = Camera.main.transform;

        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GetInput();
        
        if (_controller.isGrounded)
        {
            movementDirection = new Vector3(input.x, 0.0f, input.y);
            movementDirection *= _speed;
        }

        
        movementDirection.y -= _gravity * Time.deltaTime;

        _controller.Move(movementDirection * Time.deltaTime);
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
        if (!_controller.isGrounded) return;

        movementDirection = transform.forward * _speed;

    }
    
}
