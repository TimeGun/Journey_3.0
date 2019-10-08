using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGrower : MonoBehaviour
{
    void Awake()
    {
        smallScale = transform.localScale;
        bigScale = smallScale * 10f;
    }

    private Vector3 smallScale;

    private Vector3 bigScale;

    public bool grow;


    void Update()
    {
        if (grow)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, bigScale, Time.deltaTime);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, smallScale, Time.deltaTime);
        }
    }
}
