using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class LerpDayToNight : MonoBehaviour
{
    [SerializeField] private VolumeProfile _skyboxProfile;

    [SerializeField] private GradientSky _skybox;

    [SerializeField] private SkyboxColours _dayTime;
    [SerializeField] private SkyboxColours _nightTime;

    [SerializeField] private float lerpTimer;


    private ColorParameter _originalTop;
    private ColorParameter _originalMiddle;
    private ColorParameter _originalBottom;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        _skyboxProfile.TryGet(out _skybox);

        _originalBottom= _skybox.bottom;
        _originalMiddle = _skybox.middle;
        _originalTop = _skybox.top;


        //StartCoroutine(ChangeDayToNight(lerpTimer));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    SkyboxColours LerpSkyboxColours(SkyboxColours original, SkyboxColours target, float timeLeft)
    {
        Color newBottom = Color.Lerp(original._bottom, target._bottom, Time.deltaTime/timeLeft);
        Color newMiddle = Color.Lerp(original._middle, target._middle, Time.deltaTime/timeLeft);
        Color newTop = Color.Lerp(original._top, target._top, Time.deltaTime/timeLeft);
        
        SkyboxColours temp = new SkyboxColours(newBottom, newMiddle, newTop);

        return temp;
    }

    private IEnumerator ChangeDayToNight(float timeToLerpFor)
    {
        float timeLeft = timeToLerpFor;
        
        SkyboxColours tempColour = _dayTime;
        
        
        while (timeLeft > 0)
        {
            _skybox.gradientDiffusion.value = Mathf.Lerp(1.3f, 1f, Time.deltaTime / timeLeft);
            
             tempColour = LerpSkyboxColours(tempColour, _nightTime, timeLeft);
            _skybox.bottom.value = tempColour._bottom;
            _skybox.middle.value = tempColour._middle;
            _skybox.top.value = tempColour._top;

            timeLeft -= Time.deltaTime;
            
            yield return new WaitForEndOfFrame();
        }
    }


    private void OnDisable()
    {
        _skybox.bottom.SetValue(_originalBottom);
        _skybox.middle.SetValue(_originalMiddle);
        _skybox.top.SetValue(_originalTop);
    }
}

[System.Serializable]
public struct SkyboxColours
{
    public Color _top;
    public Color _middle;
    public Color _bottom;

    public SkyboxColours(Color bottom, Color middle, Color top)
    {
        _bottom = bottom;
        _middle = middle;
        _top = top;
    }
}