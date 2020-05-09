﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMTargetGroupAssigning : GradualLoader
{
    public CinemachineTargetGroup _targetGroup;

    public int targetIndex;
    
    
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
    
    IEnumerator Start()
    {
        yield return new WaitUntil(() => initialised);
        
        _targetGroup.m_Targets[targetIndex].target = API.GlobalReferences.PlayerRef.transform;
    }
}
