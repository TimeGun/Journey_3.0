using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMTargetGroupAssigning : MonoBehaviour
{
    public CinemachineTargetGroup _targetGroup;

    public int targetIndex;
    void Start()
    {
        _targetGroup.m_Targets[targetIndex].target = API.GlobalReferences.PlayerRef.transform;
    }
}
