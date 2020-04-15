using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AmbienceManager : MonoBehaviour
{
    public static AmbienceManager instance;


    [SerializeField] private AudioSource[] _outsideSource;

    [SerializeField] private AudioSource[] _insideSource;

    [SerializeField] private AudioSource[] _topOfTheMountainSources;

    [SerializeField] private float moveTowardsSpeed;

    [SerializeField] private AudioMixer _masterMix;
    

    void Start()
    {
        instance = this;
    }

    public void SetOutSideAmbience(float newVolume)
    {
        StartCoroutine(SetNewVolumes(_outsideSource, newVolume));
    }

    public void SetInsideAmbience(float newVolume)
    {
        StartCoroutine(SetNewVolumes(_insideSource, newVolume));

    }

    public void SetTopOfTheMountainAmbience(float newVolume)
    {
        StartCoroutine(SetNewVolumes(_topOfTheMountainSources, newVolume));
    }

    IEnumerator SetNewVolumes(AudioSource[] sources, float newVolume)
    {
        
        
        if (sources == null)
            yield break;
        
        if (!sources[0].isPlaying)
        {
            for (int i = 0; i < sources.Length; i++)
            {
                sources[i].Play();
            }
        }
        
        
        while (sources[0].volume != newVolume)
        {
            for (int i = 0; i < sources.Length; i++)
            {
                MoveVolumeTo(sources[i], newVolume);
            }
            
            yield return new WaitForEndOfFrame();
        }

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
        instance.StartCoroutine(instance.FadeInMaster(PlayerPrefs.GetFloat("MasterVolume", 0)));
    }

    IEnumerator FadeInMaster(float target)
    {
        float currentVolume;
        _masterMix.GetFloat("MasterVolume", out currentVolume);
        
        while (currentVolume < target)
        {
            print(currentVolume);
            currentVolume = Mathf.MoveTowards(currentVolume, target, Time.deltaTime * 50f);
            _masterMix.SetFloat("MasterVolume", currentVolume);
            _masterMix.GetFloat("MasterVolume", out currentVolume);
            yield return new WaitForEndOfFrame();
        }
    }
}
