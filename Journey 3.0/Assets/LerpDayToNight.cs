using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class LerpDayToNight : MonoBehaviour
{
    [SerializeField] private VolumeProfile _skyboxProfile;
    [SerializeField] private VolumeProfile _nightTimeSkyboxProfine;

    [SerializeField] private GradientSky _skybox;
    [SerializeField] private GradientSky _nightSkybox;

    [SerializeField] private float lerpTimer;


    private ColorParameter _originalTop;
    private ColorParameter _originalMiddle;
    private ColorParameter _originalBottom;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        _skyboxProfile.TryGet(out _skybox);
        _nightTimeSkyboxProfine.TryGet(out _nightSkybox);

        _originalBottom = _skybox.bottom;
        _originalMiddle = _skybox.middle;
        _originalTop = _skybox.top;

        //_skybox.bottom.value = _nightSkybox.bottom.value;
        //_skybox.middle.value = _nightSkybox.middle.value;
        //_skybox.top.value = _nightSkybox.top.value;

        StartCoroutine(ChangeDayToNight(lerpTimer));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    private IEnumerator ChangeDayToNight(float timeToLerpFor)
    {
        float timeLeft = timeToLerpFor;
        
        
        while (timeLeft > 0)
        {
            _skybox.gradientDiffusion.value = Mathf.Lerp(1.3f, 1f, Time.deltaTime / timeLeft);


            _skybox.bottom.value = Color.Lerp(_skybox.bottom.value, _nightSkybox.bottom.value, Time.deltaTime/timeLeft);
            _skybox.middle.value = Color.Lerp(_skybox.middle.value, _nightSkybox.middle.value, Time.deltaTime/timeLeft);
            _skybox.top.value = Color.Lerp(_skybox.top.value, _nightSkybox.top.value, Time.deltaTime/timeLeft);


            timeLeft -= Time.deltaTime;
            
            yield return new WaitForEndOfFrame();
        }
    }


    private void OnDestroy()
    {
        _skybox.bottom.SetValue(_originalBottom);
        _skybox.middle.SetValue(_originalMiddle);
        _skybox.top.SetValue(_originalTop);
    }
}