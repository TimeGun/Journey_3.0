﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class LeftArmIK : MonoBehaviour
{

    [SerializeField] private bool _inUse = false;
    
    [SerializeField] private bool _tempUse;

    public bool InUse
    {
        get => _inUse;
        set => _inUse = value;
    }

    public bool TempUse
    {
        get => _tempUse;
        set => _tempUse = value;
    }


    [SerializeField] private TwoBoneIKConstraint _ikConstraint;
    
    [SerializeField] private Transform target;

    [SerializeField] private Transform hint;

    public static LeftArmIK Instance;
    
    private float inUseLerpPercentage;


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (_inUse || _tempUse)
        {
            _ikConstraint.weight = Mathf.Lerp(_ikConstraint.weight, inUseLerpPercentage, Time.deltaTime * 4f);
        }
        else
        {
            _ikConstraint.weight = Mathf.Lerp(_ikConstraint.weight, 0, Time.deltaTime * 6f);
        }
    }


    public void SetIkTargetAndHint(IKSettings _settings, float lerpPercentage)
    {
        inUseLerpPercentage = lerpPercentage;
        
        target.localPosition = _settings.targetPos;
        target.localRotation = _settings.targetRot;
        hint.localPosition = _settings.hintPos;
        _inUse = true;
    }

    public void SetProceduralTargetAndHint(Vector3 position, Quaternion handRotation, float lerpPercentage)
    {
        inUseLerpPercentage = lerpPercentage;
        target.position = position;
        target.localRotation = handRotation;
    }

    public void StopIK()
    {
        _inUse = false;
    }
}