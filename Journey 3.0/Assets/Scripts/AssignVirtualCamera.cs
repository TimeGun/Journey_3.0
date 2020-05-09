using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AssignVirtualCamera : GradualLoader
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    [SerializeField] private bool sphere;

    [SerializeField] private bool assignFollow = true;
    [SerializeField] private bool assignLookAt = true;
    
    
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
        
        if (assignFollow)
        {
            if (sphere)
            {
                _virtualCamera.Follow = API.GlobalReferences.BehindPlayerSphere.transform;
            }
            else
            {
                _virtualCamera.Follow = API.GlobalReferences.PlayerRef.transform;
            }
        }

        if (assignLookAt)
        {
            _virtualCamera.LookAt = API.GlobalReferences.PlayerRef.transform;
        }
    }
    
}
