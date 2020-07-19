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
    private float masterVolumeSlider;
    private float ambianceVolumeSlider;
    private float sfxVolumeSlider;
    private float musicVolumeSlider;

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
    [SerializeField] private AudioMixer ambianceMix;
    [SerializeField] private AudioMixer sfxMix;
    [SerializeField] private AudioMixer musicMix;
    [SerializeField] private AudioMixer UIMix;

    [SerializeField] private AudioSource testSource;
    [SerializeField] private AudioClip testClip;

    // Start is called before the first frame update
    void Start()
    {
        _generalPostProcessing.TryGet(out _liftGammaGain);
        LoadSettings();
    }

    public void SetMasterVolume(float volume)
    {
        float newVolume = Mathf.Log10(volume) * 20f;
        masterMix.SetFloat("MasterVolume", newVolume);
        masterVolume = newVolume;
        //masterVolumeSlider = volume;
        testSource.outputAudioMixerGroup = UIMix.outputAudioMixerGroup;
        testSource.PlayOneShot(testClip);
        SaveSettings();
    }

    public void SetAmbianceVolume(float volume)
    {
        float newVolume = Mathf.Log10(volume) * 20f;
        masterMix.SetFloat("AmbianceVolume", newVolume);
        ambianceVolume = newVolume;
        //ambianceVolumeSlider = volume;
        testSource.outputAudioMixerGroup = ambianceMix.outputAudioMixerGroup;
        testSource.PlayOneShot(testClip);
        SaveSettings();
    }

    public void SetSFXVolume(float volume)
    {
        float newVolume = Mathf.Log10(volume) * 20f;
        masterMix.SetFloat("InteractiblesVolume", newVolume);
        masterMix.SetFloat("PlayerVolume", newVolume);
        testSource.outputAudioMixerGroup = sfxMix.outputAudioMixerGroup;
        testSource.PlayOneShot(testClip);
        sfxVolume = newVolume;
        //sfxVolumeSlider = volume;
        SaveSettings();
    }

    public void SetMusicVolume(float volume)
    {
        float newVolume = Mathf.Log10(volume) * 20f;
        masterMix.SetFloat("MusicVolume", newVolume);
        musicVolume = newVolume;
        //musicVolumeSlider = volume;
        testSource.outputAudioMixerGroup = musicMix.outputAudioMixerGroup;
        testSource.PlayOneShot(testClip);
        SaveSettings();
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
        
        testSource.outputAudioMixerGroup = UIMix.outputAudioMixerGroup;
        testSource.PlayOneShot(testClip);
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
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", Mathf.Log10(0.5f) * 20f);
        ambianceVolume = PlayerPrefs.GetFloat("AmbianceVolume", Mathf.Log10(0.25f) * 20f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", Mathf.Log10(0.5f) * 20f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", Mathf.Log10(0.5f) * 20f);

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
        
        //masterMix.SetFloat("MasterVolume", masterVolume);
        masterMix.SetFloat("AmbianceVolume", ambianceVolume);
        masterMix.SetFloat("InteractiblesVolume", sfxVolume);
        masterMix.SetFloat("PlayerVolume", sfxVolume);
        masterMix.SetFloat("MusicVolume", musicVolume);
        
        /*masterVolumeSlider = Map(masterVolume, -80f, 0f, 0.001f, 1f);
        ambianceVolumeSlider = Map(ambianceVolume, -80f, 0f, 0.001f, 1f);
        sfxVolumeSlider = Map(sfxVolume, -80f, 0f, 0.001f, 1f);
        musicVolumeSlider = Map(musicVolume, -80f, 0f, 0.001f, 1f);*/
        
        
        masterVol.value = Mathf.Pow(10f, masterVolume/20f);
        ambianceVol.value = Mathf.Pow(10f, ambianceVolume/20f);
        sfxVol.value = Mathf.Pow(10f, sfxVolume/20f);
        musicVol.value = Mathf.Pow(10f, musicVolume/20f);
    }
    
    
    public float Map(float a, float b, float c, float d, float e)
    {
        float cb = c - b;
        float de = e - d;
        float howFar = (a - b) / cb;
        return d + howFar * de;

        //float a = value you want mapped t
    }
}