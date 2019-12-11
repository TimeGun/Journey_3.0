using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateCamera : MonoBehaviour
{
    public bool test;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            FliGateBool();
        }
    }

    public void FliGateBool()
    {
        PlayerManager.GateOpened = true;
        Debug.Log("oi");
    }
}
