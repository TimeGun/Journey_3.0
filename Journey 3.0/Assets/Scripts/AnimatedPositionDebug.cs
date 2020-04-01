﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedPositionDebug : MonoBehaviour
{
    [SerializeField] private Mesh mesh;

    [SerializeField] private float positionVerticalAdjustment;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawMesh(mesh, transform.position + new Vector3(0, positionVerticalAdjustment, 0), transform.rotation);
    }
}
