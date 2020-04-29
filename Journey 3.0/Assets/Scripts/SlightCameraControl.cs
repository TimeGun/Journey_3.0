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

    private OffsetOptions _offsetOptions;

    void Start()
    {
        _inputSetUp = GetComponent<InputSetUp>();
        
        _mainCamera = API.GlobalReferences.MainCamera;

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
        
        OffsetOptions offsetOptions = _currentVirtualCamera.GetComponent<OffsetOptions>();

        CinemachineCameraOffset cameraOffset = _currentVirtualCamera.GetComponent<CinemachineCameraOffset>();
        
        if (offsetOptions != null && cameraOffset != null)
        {
            _offsetOptions = offsetOptions;
            _cameraOffset = cameraOffset;
        }
        else
        {
            _offsetOptions = null;
            _cameraOffset = null;
        }
    }
    

    IEnumerator UpdateCameraOffset()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            if(_cinemachineBrain.IsBlending || _currentVirtualCamera == null)
            {
                yield return new WaitUntil(() => !_cinemachineBrain.IsBlending);
                SetVirtualCamera();
            }

            _input = _inputSetUp.RightStick;

            if (_cameraOffset != null)
            {
                _cameraOffset.m_Offset = Vector3.Lerp(_cameraOffset.m_Offset, new Vector3(_input.x, _input.y, 0f) * _offsetOptions.OffsetMultiplier, Time.deltaTime);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
