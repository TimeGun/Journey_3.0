using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Transform cam;
    
    private Rigidbody _rb;
    
    [SerializeField] private float _speed;

    [SerializeField, Range(0.01f, 0.99f)] private float percentageVelocityToRemove = 0.1f;
    void Start()
    {
        if (cam == null)
            cam = Camera.main.transform;

        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 right = cam.right;
        
        Vector3 forward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        
        Vector3 movement = right * (Input.GetAxis("Horizontal") * _speed) + forward * (Input.GetAxis("Vertical") * _speed) ;
        

        if (movement != Vector3.zero)
        {
            _rb.AddForce(movement, ForceMode.VelocityChange);
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _speed);
            transform.rotation = Quaternion.LookRotation(_rb.velocity, transform.up);
        }
        else
        {
            _rb.velocity = _rb.velocity * (1f - percentageVelocityToRemove);
        }
    }
    
}
