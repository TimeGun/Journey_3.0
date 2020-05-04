using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionController : MonoBehaviour
{

    [SerializeField] private Color _colour;
    [SerializeField] private float _emissionIntensity;

    [SerializeField] private Material _material;

    [SerializeField] private float _emissionIntensityLerpSpeed;
    [SerializeField] private float _colourLerpSpeed;

    private Color _oldColour;

    private Coroutine _coroutine = null;

    void Start()
    {
        StartCoroutine(AdjustEmotionSettings());
    }

    public void SetColour(Color col)
    {
        print("New Colour set to: " + col);
        _colour = col;
    }
    
    public void SetTempColour(Color col, float tempDuration)
    {
        if (_coroutine == null)
        {
            _oldColour = _colour;
            print("Colour Set to: " + col);
            _coroutine = StartCoroutine(SetTempColourCoroutine(col, tempDuration));
        }
        else
        {
            StopCoroutine(_coroutine);
            print("Colour Set to: " + col);
            _coroutine = StartCoroutine(SetTempColourCoroutine(col, tempDuration));
        }
    }

    IEnumerator SetTempColourCoroutine(Color col, float tempDuration)
    {
        SetColour(col);
        
        yield return new WaitForSeconds(tempDuration);
        
        SetColour(_oldColour);
        _coroutine = null;
    }

    public void SetEmissionIntensity(float intensity)
    {
        _emissionIntensity = intensity;
    }

    private IEnumerator AdjustEmotionSettings()
    {
        while (true)
        {
            if (_material.GetColor("_emissionColour") != _colour)
            {
                _material.SetColor("_emissionColour", Color.Lerp(_material.GetColor("_emissionColour"), _colour, _colourLerpSpeed * Time.deltaTime));
            }

            if (_material.GetFloat("_emissionIntensity") != _emissionIntensity)
            {
                _material.SetFloat("_emissionIntensity", Mathf.Lerp(_material.GetFloat("_emissionIntensity"), _emissionIntensity, _emissionIntensityLerpSpeed * Time.deltaTime));
            }
            
            yield return new WaitForEndOfFrame();
        }
    }
    
    
    
}
