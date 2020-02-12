using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignSphereRef : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        API.GlobalReferences.BehindPlayerSphere = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
