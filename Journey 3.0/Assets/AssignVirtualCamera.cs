using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AssignVirtualCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    void Start()
    {
        _virtualCamera.Follow = API.GlobalReferences.PlayerRef.transform;
        _virtualCamera.LookAt = API.GlobalReferences.PlayerRef.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
