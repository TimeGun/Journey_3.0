using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 inputAxis;

    [SerializeField] private float speed;
    
    [SerializeField] private float maxSpeed;

    [SerializeField] private float damping = 1f;
    
    private Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        
        
        inputAxis = new Vector3(xAxis, 0f,yAxis);
        
        rb.AddForce( speed * Time.deltaTime * inputAxis , ForceMode.VelocityChange);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        if (new Vector2(xAxis, yAxis) == Vector2.zero)
        {
            rb.velocity *= (1.0f - (damping * Time.deltaTime));
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity, transform.up);
        }

    }
}
