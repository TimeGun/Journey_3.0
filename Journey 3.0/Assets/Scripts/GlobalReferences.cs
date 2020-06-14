using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
    private GameObject _playerRef;
    private GameObject _mainCamera;
    private GameObject _behindPlayerSphere;
    private GameObject _underGlyf;

    public GameObject BehindPlayerSphere
    {
        get => _behindPlayerSphere;
        set => _behindPlayerSphere = value;
    }

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

    public GameObject UnderGlyf
    {
        get => _underGlyf;
        set => _underGlyf = value;
    }
}