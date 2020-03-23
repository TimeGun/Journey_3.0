using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SlightCameraControl : MonoBehaviour
{
    private InputSetUp _inputSetUp;

    private Vector2 _input;

    [SerializeField] private GameObject _mainCamera;

    [SerializeField] private CinemachineCameraOffset _cameraOffset;

    [SerializeField] private CinemachineBrain _cinemachineBrain;
    
    [SerializeField] private GameObject _currentVirtualCamera;
    
    
    void Start()
    {
        _inputSetUp = GetComponent<InputSetUp>();
        //_mainCamera = API.GlobalReferences.MainCamera;

        if (_mainCamera == null)
        {
            _mainCamera = Camera.main.gameObject;
        }

        _cinemachineBrain = _mainCamera.GetComponent<CinemachineBrain>();
        
        SetVirtualCamera();
        
        StartCoroutine(UdateCameraOffset());
    }

    private void SetVirtualCamera()
    {
        _currentVirtualCamera =
            _cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject;
        _cameraOffset = _currentVirtualCamera.GetComponent<CinemachineCameraOffset>();
    }
    

    IEnumerator UdateCameraOffset()
    {
        while (true)
        {
            print("what??");
            
            if(_cinemachineBrain.IsBlending)
            {
                print("yeet, yeet mufuka");
                yield return new WaitUntil(() => !_cinemachineBrain.IsBlending);
                SetVirtualCamera();
            }

            _input = _inputSetUp.RightStick;
            
        
            _cameraOffset.m_Offset = Vector3.Lerp(_cameraOffset.m_Offset, new Vector3(_input.x, _input.y, 0f), Time.deltaTime);
            
            yield return new WaitForEndOfFrame();
        }
    }
}
