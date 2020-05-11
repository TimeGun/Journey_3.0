using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutVolume : MonoBehaviour
{
    [SerializeField] private AudioSource _engineSource;
    [SerializeField] private float fadeSpeed = 0.75f;

    public void FadeOut()
    {
        StartCoroutine(FadeOutEngine());
    }

    IEnumerator FadeOutEngine()
    {
        while (_engineSource.volume > 0)
        {
            _engineSource.volume = Mathf.Lerp(_engineSource.volume, 0, Time.deltaTime * fadeSpeed);
            yield return null;
        }
    }
}
