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
        
        StartCoroutine(UpdateCameraOffset());
    }

    private void SetVirtualCamera()
    {
        _currentVirtualCamera =
            _cinemachineBrain.ActiveVirtualCamera.VirtualCameraGameObject;
        _cameraOffset = _currentVirtualCamera.GetComponent<CinemachineCameraOffset>();
    }
    

    IEnumerator UpdateCameraOffset()
    {
        
        
        while (true)
        {
            if(_cinemachineBrain.IsBlending || _currentVirtualCamera == null)
            {
                yield return new WaitUntil(() => !_cinemachineBrain.IsBlending);
                SetVirtualCamera();
            }

            _input = _inputSetUp.RightStick;
            
        
            _cameraOffset.m_Offset = Vector3.Lerp(_cameraOffset.m_Offset, new Vector3(_input.x, _input.y, 0f) * _cameraOffset.m_offsetMultiplier, Time.deltaTime);
            
            yield return new WaitForEndOfFrame();
        }
    }
}
