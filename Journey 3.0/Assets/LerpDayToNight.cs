using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class LerpDayToNight : MonoBehaviour
{
    [SerializeField] private VolumeProfile _dayTimeSkyboxProfile;
    [SerializeField] private VolumeProfile _nightTimeSkyboxProfile;
    [SerializeField] private VolumeProfile _changingSkyboxProfile;

    [SerializeField] private GradientSky _daySkybox;
    [SerializeField] private GradientSky _nightSkybox;
    [SerializeField] private GradientSky _changingSkybox;
    
    public static LerpDayToNight instance;

    [SerializeField] private float debugLerpTime;

    public bool debug;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        EditorApplication.playmodeStateChanged += ModeChanged;
#endif
        _dayTimeSkyboxProfile.TryGet(out _daySkybox);
        _nightTimeSkyboxProfile.TryGet(out _nightSkybox);


        _changingSkyboxProfile.TryGet(out _changingSkybox);


        _changingSkybox.bottom.value = _daySkybox.bottom.value;
        _changingSkybox.middle.value = _daySkybox.middle.value;
        _changingSkybox.top.value = _daySkybox.top.value;

        instance = this;

        if (debug)
        {
            StartCoroutine(ChangeDayToNight(debugLerpTime));
        }
    }

    public static void LerpToNight(float secondsToLerpFor)
    {
        instance.StartCoroutine(instance.ChangeDayToNight(secondsToLerpFor));
    }

    public static void SetToNight()
    {
        instance._changingSkybox.bottom.value = instance._nightSkybox.bottom.value;
        instance._changingSkybox.middle.value = instance._nightSkybox.middle.value;
        instance._changingSkybox.top.value = instance._nightSkybox.top.value;
    }


    private IEnumerator ChangeDayToNight(float timeToLerpFor)
    {
        float timeLeft = timeToLerpFor;


        while (timeLeft > 0)
        {
            print("gas");
            _changingSkybox.gradientDiffusion.value = Mathf.Lerp(_changingSkybox.gradientDiffusion.value,
                _nightSkybox.gradientDiffusion.value, Time.deltaTime / timeLeft);


            _changingSkybox.bottom.value = Color.Lerp(_changingSkybox.bottom.value, _nightSkybox.bottom.value,
                Time.deltaTime / timeLeft);
            _changingSkybox.middle.value = Color.Lerp(_changingSkybox.middle.value, _nightSkybox.middle.value,
                Time.deltaTime / timeLeft);
            _changingSkybox.top.value =
                Color.Lerp(_changingSkybox.top.value, _nightSkybox.top.value, Time.deltaTime / timeLeft);


            timeLeft -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }

#if UNITY_EDITOR
    

    void ModeChanged()
    {
        if (!EditorApplication.isPlayingOrWillChangePlaymode &&
            EditorApplication.isPlaying)
        {
            _changingSkybox.bottom.value = _daySkybox.bottom.value;
            _changingSkybox.middle.value = _daySkybox.middle.value;
            _changingSkybox.top.value = _daySkybox.top.value;
        }
    }
#endif
}