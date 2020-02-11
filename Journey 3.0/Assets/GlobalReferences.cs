using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    private GameObject _playerRef;
    private GameObject _mainCamera;

    public GameObject PlayerRef
    {
        get => _playerRef;
        set => _playerRef = value;
    }

    public GameObject MainCamera
    {
        get => _mainCamera;
        set => _mainCamera = value;
    }
}