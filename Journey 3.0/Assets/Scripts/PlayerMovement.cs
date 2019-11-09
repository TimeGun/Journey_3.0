using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. Movement based of camera
/// 2. stop and face dircetion when input is absent
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Transform cam;
    
    private Rigidbody _rb;
    
    [SerializeField] private float _velocity = 5f;
    [SerializeField] private float _turnSpeed = 10f;

    private Vector2 input;

    private float angle;

    private Quaternion targetRotation;
    
 
    
    
    void Start()
    {
        if (cam == null)
            cam = Camera.main.transform;

        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetInput();
        
        if(Mathf.Abs(input.x) < 0.1f && Mathf.Abs(input.y) < 0.1f) return;
        
        CalculateDirection();
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
        //_rb.velocity += transform.forward * _velocity;
        //_rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _velocity);

        transform.position += transform.forward * _velocity * Time.deltaTime;
        
    }
}
