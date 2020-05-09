using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript: GradualLoader
{
    public override void EnqueThis()
    {
        
        base.EnqueThis();
    }

    public override void InitialiseThis()
    {
        base.InitialiseThis();
    }

    public override void Awake()
    {
        print("Called Awake");
        base.Awake();
    }

    private void Update()
    {
        if (initialised)
        {
            print("Update");
        }
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => initialised);
        print("called start");
    }

}