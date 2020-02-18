using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AssignVirtualCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    [SerializeField] private bool sphere;

    [SerializeField] private bool assignFollow = true;
    [SerializeField] private bool assignLookAt = true;
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
