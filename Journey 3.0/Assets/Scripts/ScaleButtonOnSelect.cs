using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScaleButtonOnSelect : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler,
    IDeselectHandler
{
    [SerializeField] private float _timeToScaleFor = 0.1f;

    [SerializeField] private float _scaleMultiplier = 1.2f;

    private Vector3 _startScale;


    private Coroutine _upScaler;

    private Coroutine _downScaler;

    private Button button;


    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _clip;

    void PlayAudioSource()
    {
        if (!_source.isPlaying)
        {
            _source.PlayOneShot(_clip);
        }
    }


    void Start()
    {
        _startScale = transform.localScale;
        button = GetComponent<Button>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (button.interactable)
        {
            if (_downScaler != null)
            {
                StopCoroutine(_downScaler);
                _downScaler = null;
            }

            if (_upScaler == null)
            {
                _upScaler = StartCoroutine(ScaleUp());
            }

            PlayAudioSource();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);

            if (_downScaler != null)
            {
                StopCoroutine(_downScaler);
                _downScaler = null;
            }

            if (_upScaler == null)
            {
                _upScaler = StartCoroutine(ScaleUp());
            }

            PlayAudioSource();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_upScaler != null)
        {
            StopCoroutine(_upScaler);
            _upScaler = null;
        }

        if (_downScaler == null)
        {
            _downScaler = StartCoroutine(ScaleDown());
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        GetComponent<Selectable>().OnPointerExit(null);

        if (_upScaler != null)
        {
            StopCoroutine(_upScaler);
            _upScaler = null;
        }

        if (_downScaler == null)
        {
            _downScaler = StartCoroutine(ScaleDown());
        }
    }

    private IEnumerator ScaleUp()
    {
        yield return new WaitForEndOfFrame();


        float timeLeft = _timeToScaleFor;

        while (transform.localScale != _startScale * _scaleMultiplier)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _startScale * _scaleMultiplier,
                Time.unscaledDeltaTime / timeLeft);

            timeLeft -= Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }

        print("Finished Up Scale");
    }

    private IEnumerator ScaleDown()
    {
        float timeLeft = _timeToScaleFor;

        while (transform.localScale != _startScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _startScale,
                Time.unscaledDeltaTime / timeLeft);

            timeLeft -= Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }

        print("Finished Down Scale");
    }


    private void OnDisable()
    {
        StopAllCoroutines();
        transform.localScale = _startScale;
    }

    private void OnEnable()
    {
        _startScale = transform.localScale;
    }
}