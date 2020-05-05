using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class RightArmIK : MonoBehaviour
{

    public bool debugPosition;
    
    [SerializeField] private bool _inUse = false;

    public bool InUse
    {
        get => _inUse;
        set => _inUse = value;
    }

    [SerializeField] private bool _tempUse;

    public bool TempUse
    {
        get => _tempUse;
        set => _tempUse = value;
    }

    [SerializeField] private TwoBoneIKConstraint _ikConstraint;
    
    [SerializeField] private Transform target;

    [SerializeField] private Transform hint;

    public static RightArmIK Instance;

    private IKSettings lastIkSetting;

    private float inUseLerpPercentage;


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (debugPosition && lastIkSetting != null)
        {
            SetIkTargetAndHint(lastIkSetting, inUseLerpPercentage);
        }

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
        lastIkSetting = _settings;
        target.localPosition = _settings.targetPos;
        target.localRotation = _settings.targetRot;
        hint.localPosition = _settings.hintPos;
        _inUse = true;
    }

    public void SetProceduralTargetAndHint(Vector3 position, Vector3 normal, float lerpPercentage)
    {
        inUseLerpPercentage = lerpPercentage;
        target.position = position;
        target.rotation =
            Quaternion.FromToRotation(Vector3.up,
                normal) * Quaternion.LookRotation(normal, Vector3.up)
                        * Quaternion.AngleAxis(-90f, Vector3.up);
    }

    public void StopIK()
    {
        _inUse = false;
    }
}

