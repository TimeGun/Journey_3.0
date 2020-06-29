using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GradualLoader: MonoBehaviour, IGradualLoader
{
    protected bool initialised = false;
    protected bool update = false;
    
    public virtual void EnqueThis()
    {
        GradualLoaderManager.Behaviours.Enqueue(this);
    }

    public virtual void InitialiseThis()
    {
        initialised = true;
        StartCoroutine(UpdateThis());
    }
    
    public virtual IEnumerator UpdateThis()
    {
        yield return null;
        update = true;
    }

    public virtual void Awake()
    {
        EnqueThis();
    }
}


public interface IGradualLoader
{
    void EnqueThis();

    void InitialiseThis();

    void Awake();
    
}

/*
 //How to Set up the gradual loader in each relevant class
 // The function below are required
 
    public override void EnqueThis()
    {
        print("Enqued This");
        base.EnqueThis();
    }

    public override void InitialiseThis()
    {
        print("Initialised This");
        base.InitialiseThis();
    }
    
    public override void Awake()
    {
        print("Called Awake");
        base.Awake();
    }
    
    void Update()
    {
        if (initialised)
        {
        }
    }
    
    IEnumerator Start()
    {
        yield return new WaitUntil(() => initialised);
    }
    
 */
