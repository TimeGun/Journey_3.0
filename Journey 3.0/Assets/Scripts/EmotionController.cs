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


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AdjustEmotionSettings());
    }

    public void SetColour(Color col)
    {
        _colour = col;
    }
    
    public void SetTempColour(Color col, float tempDuration)
    {
        StartCoroutine(SetTempColourCoroutine(col, tempDuration));
    }

    IEnumerator SetTempColourCoroutine(Color col, float tempDuration)
    {
        Color pastColour = _colour;
        
        SetColour(col);
        
        yield return new WaitForSeconds(tempDuration);
        
        SetColour(pastColour);
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
