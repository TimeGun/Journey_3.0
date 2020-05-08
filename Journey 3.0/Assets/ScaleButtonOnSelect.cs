using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleButtonOnSelect : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler, IDeselectHandler
{
    private float _timeToScaleFor = 0.25f;

    [SerializeField] private float _scaleMultiplier = 1.25f;
    
    private Vector3 _startScale;
    

    private Coroutine _upScaler;
    
    private Coroutine _downScaler;

    void Start()
    {
        _startScale = transform.localScale;
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (_downScaler != null)
        {
            StopCoroutine(_downScaler);
        }
        
        if (_upScaler == null)
        {
            _upScaler = StartCoroutine(ScaleUp());
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_downScaler != null)
        {
            StopCoroutine(_downScaler);
        }
        
        if (_upScaler == null)
        {
            _upScaler = StartCoroutine(ScaleUp());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_upScaler != null)
        {
            StopCoroutine(_upScaler);
        }

        if (_downScaler == null)
        {
            _downScaler = StartCoroutine(ScaleDown());
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (_upScaler != null)
        {
            StopCoroutine(_upScaler);
        }
        
        if (_downScaler == null)
        {
            _downScaler = StartCoroutine(ScaleDown());
        }
    }

    private IEnumerator ScaleUp()
    {
        float timeLeft = _timeToScaleFor;
        
        while (transform.localScale != _startScale * _scaleMultiplier)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _startScale * _scaleMultiplier,
                Time.deltaTime / timeLeft);
            
            timeLeft -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    
    private IEnumerator ScaleDown()
    {
        float timeLeft = _timeToScaleFor;
        
        while (transform.localScale != _startScale * _scaleMultiplier)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _startScale,
                Time.deltaTime / timeLeft);
            
            timeLeft -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
