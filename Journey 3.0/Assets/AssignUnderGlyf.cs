using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignUnderGlyf : MonoBehaviour
{

  
    // Start is called before the first frame update
    void Start()
    {
        API.GlobalReferences.UnderGlyf = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
