using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GrowObject : MonoBehaviour, IInteractible, IRune
{
    [SerializeField] private ObjectsOnAltarPlate _objectsOnAltarPlate;

    [SerializeField] private VisualEffect[] firePits;

    [SerializeField] private Material _runeMat, _plateMat;
    
    [SerializeField] private Color _emissionColour;

    [SerializeField] private float _minEmissionStrength = 0f;
    [SerializeField] private float _maxEmissionStrength = 10f;
    
    private float _multiplierRune;
    private float _multiplierPlate;
    
    [SerializeField] private float _lerpSpeed = 5f;


    [SerializeField] private FadeLight _fadeLight;
    
    void Start()
    {
        _multiplierRune = _maxEmissionStrength;
        _multiplierPlate = _minEmissionStrength;

        _runeMat.SetColor("_EmissiveColor", _emissionColour * _multiplierRune);
        _plateMat.SetColor("_EmissiveColor", _emissionColour * _multiplierPlate);
    }
    

    public bool isActive()
    {
        return true;
    }

    public void StartInteraction(Transform parent)
    {
        StartCoroutine(GrowObjectCoroutine(parent));
    }

    public void StopInteraction()
    {
        
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    IEnumerator GrowObjectCoroutine(Transform player)
    {
        _fadeLight.enabled = false;
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        InteractWithObject interactWithObject = player.GetComponent<InteractWithObject>();


        firePits[0].SetBool("RuneUsed", true);
        firePits[1].SetBool("RuneUsed", true);
        
        
        while (_multiplierRune != _minEmissionStrength)
        {
            print("Running");
            _multiplierRune = Mathf.MoveTowards(_multiplierRune, _minEmissionStrength, Time.deltaTime * _lerpSpeed);
            _runeMat.SetColor("_EmissiveColor", _emissionColour * _multiplierRune);
            yield return new WaitForEndOfFrame();
        }

        while (_multiplierPlate != _maxEmissionStrength)
        {
            print("Running");
            _multiplierPlate = Mathf.MoveTowards(_multiplierPlate, _maxEmissionStrength, Time.deltaTime * _lerpSpeed);
            _plateMat.SetColor("_EmissiveColor", _emissionColour * _multiplierPlate);
            yield return new WaitForEndOfFrame();
        }
        
        

        for (int i = 0; i < _objectsOnAltarPlate.ItemsOnAltar.Count; i++)
        {
            ChangeSize changeSize = _objectsOnAltarPlate.ItemsOnAltar[i].GetComponent<ChangeSize>();
            
            if (changeSize != null)
            {
                changeSize.StartChangeSize();
            }
        }
        
        

        while (_multiplierPlate != _minEmissionStrength)
        {
            print("Running");
            _multiplierPlate = Mathf.MoveTowards(_multiplierPlate, _minEmissionStrength, Time.deltaTime * _lerpSpeed);
            _plateMat.SetColor("_EmissiveColor", _emissionColour * _multiplierPlate);
            yield return new WaitForEndOfFrame();
        }
        
        while (_multiplierRune != _maxEmissionStrength)
        {
            print("Running");
            _multiplierRune = Mathf.MoveTowards(_multiplierRune, _maxEmissionStrength, Time.deltaTime * _lerpSpeed);
            _runeMat.SetColor("_EmissiveColor", _emissionColour * _multiplierRune);
            yield return new WaitForEndOfFrame();
        }
        

        firePits[0].SetBool("RuneUsed", false);
        firePits[1].SetBool("RuneUsed", false);
        
        interactWithObject.Cooldown = false;
        movement.enabled = true;
        _fadeLight.enabled = true;
        
        yield return null;
    }
}
