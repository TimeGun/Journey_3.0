using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GradualLoader: MonoBehaviour, IGradualLoader
{
    protected bool initialised = false;
    public virtual void EnqueThis()
    {
        GradualLoaderManager.Behaviours.Enqueue(this);
    }

    public virtual void InitialiseThis()
    {
        initialised = true;
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
