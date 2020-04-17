using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiblePainting : MonoBehaviour, IInteractible, IRune
{
    [SerializeField] private Material _plateMat, _frameMat;
    [SerializeField] private Animator _paintingAnimator;

    private bool paintingIsPlaying;

    [SerializeField] private string _paintingAnimationName;

    [SerializeField] private Color _emissionColour;

    [SerializeField] private float _minEmissionStrength;
    [SerializeField] private float _maxEmissionStrength;

    [SerializeField] private float _lerpSpeed = 1f;

    private float _multiplierPlate;
    private float _multiplierFrame;


    // Start is called before the first frame update
    void Start()
    {
        _multiplierPlate = _maxEmissionStrength;
        _multiplierFrame = _minEmissionStrength;

        _plateMat.SetColor("_EmissiveColor", _emissionColour * _multiplierPlate);
        _frameMat.SetColor("_EmissiveColor", _emissionColour * _multiplierFrame);
        StartCoroutine(PlayPainting());
    }

    IEnumerator PlayPainting()
    {
        while (_multiplierPlate != _minEmissionStrength)
        {
            print("Running");
            _multiplierPlate = Mathf.MoveTowards(_multiplierPlate, _minEmissionStrength, Time.deltaTime * _lerpSpeed);
            _plateMat.SetColor("_EmissiveColor", _emissionColour * _multiplierPlate);
            yield return new WaitForEndOfFrame();
        }
        
        while (_multiplierFrame != _maxEmissionStrength)
        {
            print("Running");
            _multiplierFrame = Mathf.MoveTowards(_multiplierFrame, _maxEmissionStrength, Time.deltaTime * _lerpSpeed);
            _frameMat.SetColor("_EmissiveColor", _emissionColour * _multiplierFrame);
            yield return new WaitForEndOfFrame();
        }


        _paintingAnimator.Play(_paintingAnimationName);
        yield return new WaitForEndOfFrame();
        print(_paintingAnimator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(_paintingAnimator.GetCurrentAnimatorStateInfo(0).length);

        while (_multiplierFrame != _minEmissionStrength)
        {
            print("Running");
            _multiplierFrame = Mathf.MoveTowards(_multiplierFrame, _minEmissionStrength, Time.deltaTime * _lerpSpeed);
            _frameMat.SetColor("_EmissiveColor", _emissionColour * _multiplierFrame);
            yield return new WaitForEndOfFrame();
        }
        
        while (_multiplierPlate != _maxEmissionStrength)
        {
            print("Running");
            _multiplierPlate = Mathf.MoveTowards(_multiplierPlate, _maxEmissionStrength, Time.deltaTime * _lerpSpeed);
            _plateMat.SetColor("_EmissiveColor", _emissionColour * _multiplierPlate);
            yield return new WaitForEndOfFrame();
        }
    }

    GameObject IInteractible.getGameObject()
    {
        throw new System.NotImplementedException();
    }

    public bool isActive()
    {
        throw new System.NotImplementedException();
    }

    public void StartInteraction(Transform parent)
    {
        StartCoroutine(PlayPainting());
    }

    public void StopInteraction()
    {
        throw new System.NotImplementedException();
    }

    GameObject IRune.getGameObject()
    {
        throw new System.NotImplementedException();
    }
}