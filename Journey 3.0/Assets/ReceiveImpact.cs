using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveImpact : MonoBehaviour
{
    float mass = 3.0f; // defines the character mass
    Vector3 impact = Vector3.zero;
    
    private CharacterController _characterController;
 
    void Start(){
        _characterController = GetComponent<CharacterController>();
    }
 
    // call this function to add an impact force:
    public void AddImpact(Vector3 direction, float forceToApply){
        direction.Normalize();
        //if (direction.y < 0) direction.y = -direction.y; // reflect down force on the ground
        impact += direction.normalized * forceToApply / mass;
    }
 
    void Update(){
        // apply the impact force:
        if (impact.magnitude > 0.2) _characterController.Move(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);
    }
}
