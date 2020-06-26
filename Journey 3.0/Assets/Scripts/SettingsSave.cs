using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class SettingsSave : MonoBehaviour
{
    private float masterVolume;
    private float ambianceVolume;
    private float sfxVolume;
    private float musicVolume;

    private float brightness;
    private int vsync;

    [SerializeField] private VolumeProfile _generalPostProcessing;

    private LiftGammaGain _liftGammaGain;


    [SerializeField] private Slider masterVol;
    [SerializeField] private Slider ambianceVol;
    [SerializeField] private Slider sfxVol;
    [SerializeField] private Slider musicVol;
    
    
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Toggle vsynToggle;


    [SerializeField] private AudioMixer masterMix;

    // Start is called before the first frame update
    void Start()
    {
        _generalPostProcessing.TryGet(out _liftGammaGain);
        LoadSettings();
    }

    public void SetMasterVolume(float volume)
    {
        masterMix.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f);
        masterVolume = volume;
    }

    public void SetAmbianceVolume(float volume)
    {
        masterMix.SetFloat("AmbianceVolume", Mathf.Log10(volume) * 20f);
        ambianceVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        masterMix.SetFloat("InteractiblesVolume", Mathf.Log10(volume) * 20f);
        masterMix.SetFloat("PlayerVolume", Mathf.Log10(volume) * 20f);
        sfxVolume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        masterMix.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);
        musicVolume = volume;
    }

    public void SetVSync(bool value)
    {
        if (value == true)
        {
            QualitySettings.vSyncCount = 1;
            vsync = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            vsync = 0;
        }
    }


    public void SetBrightness(float value)
    {
        brightness = value;
        _liftGammaGain.gain.value = new Vector4(brightness, brightness,brightness, brightness);
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("AmbianceVolume", ambianceVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        
        PlayerPrefs.SetFloat("Brightness", brightness);
        PlayerPrefs.SetInt("VSync", vsync);

        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0);
        ambianceVolume = PlayerPrefs.GetFloat("AmbianceVolume", 0);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0);

        brightness = PlayerPrefs.GetFloat("Brightness", 1);
        vsync = PlayerPrefs.GetInt("VSync", 0);

        if (vsync == 0)
        {
            vsynToggle.isOn = false;
        }
        else
        {
            vsynToggle.isOn = true;
        }
        
        SetVSync(vsynToggle.isOn);
        
        _liftGammaGain.gain.value = new Vector4(brightness, brightness,brightness, brightness);

        brightnessSlider.value = brightness;
        
        masterMix.SetFloat("AmbianceVolume", ambianceVolume);
        masterMix.SetFloat("InteractiblesVolume", sfxVolume);
        masterMix.SetFloat("PlayerVolume", sfxVolume);
        masterMix.SetFloat("MusicVolume", musicVolume);

        masterVol.value = masterVolume;
        ambianceVol.value = ambianceVolume;
        sfxVol.value = sfxVolume;
        musicVol.value = musicVolume;
    }
}