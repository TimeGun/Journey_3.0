using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FadeLight : MonoBehaviour
{
    public InteractWithObject _interactWithObject;
    
    public Material _runeMaterial;
    public Material _plateMaterial;
    public Light _light;

    private Color startColor;
    private Color brightColor;

    private float multiplier;

    private float startIntensity;
    
    private float intensityMultiplier = 1f;

    [SerializeField] private float _maxEmissionStrength = 10f;
    [SerializeField] private float _minEmissionStrength = 0f;
    [SerializeField] private float _minRuneEmissionStrength = 0f;

    [SerializeField] private ObjectsOnAltarPlate _objectsOnAltarPlate;

    [SerializeField] private Color _emissionColour;

    [SerializeField] private float _lerpSpeed = 5f;

    private float _multiplierPlate;

    [Range(0.1f, 10f)] [SerializeField] private float _pingPongSpeed = 1f;
    [Range(0.1f, 10f)] [SerializeField] private float _maxPingPongValue = 1f;

    [SerializeField] private Color redFire;
    [SerializeField] private Color blueFire;

    [SerializeField] private Light[] pitLights;
    

    [SerializeField] private VisualEffect[] pits;

    private void Start()
    {
        startColor = _emissionColour;
        startIntensity = _light.intensity;
        _interactWithObject = API.GlobalReferences.PlayerRef.GetComponent<InteractWithObject>();
    }

    void Update()
    {
        brightColor = startColor * multiplier;
        
        _runeMaterial.SetColor("_EmissiveColor", brightColor);
        _light.intensity = startIntensity * intensityMultiplier;
        
        
        if (_interactWithObject.NearRune && _interactWithObject.closeRune == gameObject)
        {
            multiplier = Mathf.Lerp(multiplier, _maxEmissionStrength, Time.deltaTime * _lerpSpeed);
            intensityMultiplier = Mathf.Lerp(intensityMultiplier, 5f, Time.deltaTime * _lerpSpeed);
        }
        else
        {
            multiplier = Mathf.Lerp(multiplier, _minRuneEmissionStrength, Time.deltaTime * _lerpSpeed);
            intensityMultiplier = Mathf.Lerp(intensityMultiplier, 1f, Time.deltaTime * _lerpSpeed);
        }


        if (_objectsOnAltarPlate.ItemsOnAltar.Count > 0)
        {
            if (_multiplierPlate != _maxEmissionStrength)
            {
                print("Running");
                _multiplierPlate = Mathf.MoveTowards(_multiplierPlate, _maxEmissionStrength, Time.deltaTime * _lerpSpeed);
                _plateMaterial.SetColor("_EmissiveColor", _emissionColour * _multiplierPlate);
            }
            _minRuneEmissionStrength = Mathf.PingPong(Time.time * _pingPongSpeed, _maxPingPongValue);
            pits[0].SetBool("ColourSwitch", true);
            pits[1].SetBool("ColourSwitch", true);
            pitLights[0].color = Color.Lerp(pitLights[0].color, blueFire, Time.deltaTime * _lerpSpeed);
            pitLights[1].color = Color.Lerp(pitLights[1].color, blueFire, Time.deltaTime * _lerpSpeed);
            
            //print(_minRuneEmissionStrength);
        }
        else
        {
            if (_multiplierPlate != _minEmissionStrength)
            {
                _multiplierPlate = Mathf.MoveTowards(_multiplierPlate, _minEmissionStrength, Time.deltaTime * _lerpSpeed * 3f);
                _plateMaterial.SetColor("_EmissiveColor", _emissionColour * _multiplierPlate);
                
            }
            pits[0].SetBool("ColourSwitch", false);
            pits[1].SetBool("ColourSwitch", false);
            pitLights[0].color = Color.Lerp(pitLights[0].color, redFire, Time.deltaTime * _lerpSpeed);
            pitLights[1].color = Color.Lerp(pitLights[1].color, redFire, Time.deltaTime * _lerpSpeed);
            
            
            _minRuneEmissionStrength = Mathf.Lerp(_minRuneEmissionStrength, 0, Time.deltaTime * _lerpSpeed);
        }
    }
}
