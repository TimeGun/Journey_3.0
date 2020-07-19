using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AmbienceManager : MonoBehaviour
{
    public static AmbienceManager instance;

    [SerializeField] private AudioProfileScriptableObject[] _profiles;


    [SerializeField] private AudioSource[] _outsideSources;

    [SerializeField] private AudioSource[] _insideSources;

    [SerializeField] private AudioSource[] _topOfTheMountainSources;

    [SerializeField] private float moveTowardsSpeed;

    [SerializeField] private AudioMixer _masterMix;

    [SerializeField] private float _quietModifier;


    [SerializeField] public ScatterSounds[] _scatterIntruments;
    

    void Start()
    {
        instance = this;
    }

    public void SetProfile(int profileIndex)
    {
        SetOutSideAmbience(_profiles[profileIndex].outsideVol);
        SetInsideAmbience(_profiles[profileIndex].insideVol);
        SetTopOfTheMountainAmbience(_profiles[profileIndex].mountainVol);
    }

    public void SetOutSideAmbience(float newVolume)
    {
        StartCoroutine(SetNewVolumes(_outsideSources, newVolume));
        _scatterIntruments[0].startVolume = newVolume;
    }

    public void SetInsideAmbience(float newVolume)
    {
        StartCoroutine(SetNewVolumes(_insideSources, newVolume));
        _scatterIntruments[1].startVolume = newVolume;
    }

    public void SetTopOfTheMountainAmbience(float newVolume)
    {
        StartCoroutine(SetNewVolumes(_topOfTheMountainSources, newVolume));
        _scatterIntruments[2].startVolume = newVolume;
        _scatterIntruments[3].startVolume = newVolume;
    }

    IEnumerator SetNewVolumes(AudioSource[] sources, float newVolume)
    {
        //check if audio sources exist
        if (sources == null)
            yield break;
        
        //if the sources are off, turn them on
        if (!sources[0].isPlaying)
        {
            for (int i = 0; i < sources.Length; i++)
            {
                sources[i].Play();
            }
        }
        
        
        //move the volume to the desired value
        while (sources[0].volume != newVolume)
        {
            for (int i = 0; i < sources.Length; i++)
            {
                MoveVolumeTo(sources[i], newVolume);
            }
            
            yield return new WaitForEndOfFrame();
        }
        
        //if the volume is zero, turn off the source
        if (newVolume == 0)
        {
            for (int i = 0; i < sources.Length; i++)
            {
                sources[i].Stop();
            }
        }
    }


    void MoveVolumeTo(AudioSource source, float targetVolume)
    {
        source.volume = Mathf.MoveTowards(source.volume, targetVolume, Time.deltaTime * moveTowardsSpeed);
    }



    public static void FadeInMasterSound()
    {
        float targetVol = PlayerPrefs.GetFloat("MasterVolume", Mathf.Log10(0.5f) * 20f);
        instance.StartCoroutine(instance.FadeInMaster(targetVol));
    }
    
    public static void FadeOutMasterSound()
    {
        instance.StartCoroutine(instance.FadeOutMaster(-80f));
    }
    
    

    IEnumerator FadeInMaster(float target)
    {
        float currentVolume;
        _masterMix.GetFloat("MasterVolume", out currentVolume);
        while (currentVolume < target)
        {
            currentVolume = Mathf.MoveTowards(currentVolume, target, Time.deltaTime * 10f);
            _masterMix.SetFloat("MasterVolume", currentVolume);
            _masterMix.GetFloat("MasterVolume", out currentVolume);
            yield return new WaitForEndOfFrame();
        }
    }
    
    IEnumerator FadeOutMaster(float target)
    {
        float currentVolume;
        _masterMix.GetFloat("MasterVolume", out currentVolume);
        
        while (currentVolume > target)
        {
            currentVolume = Mathf.MoveTowards(currentVolume, target, Time.deltaTime * 50f);
            _masterMix.SetFloat("MasterVolume", currentVolume);
            _masterMix.GetFloat("MasterVolume", out currentVolume);
            yield return new WaitForEndOfFrame();
        }
    }
}
