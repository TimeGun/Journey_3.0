using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlayerClose : MonoBehaviour
{
    private bool _playerNearRune;

    public InteractWithObject _interactWithObject;

    private LineRenderer _lineRenderer;

    [SerializeField] private float lerpSpeed = 10f;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerNearRune = _interactWithObject.NearRune;

        _lineRenderer.SetPosition(0, transform.position);
        if (_playerNearRune)
        {
            _lineRenderer.SetPosition(1, Vector3.Lerp(_lineRenderer.GetPosition(1), _interactWithObject._chestHeight.position, Time.deltaTime * lerpSpeed * 1.5f));
        }
        else
        {
            _lineRenderer.SetPosition(1,
                Vector3.Lerp(_lineRenderer.GetPosition(1), transform.position, Time.deltaTime * lerpSpeed));
        }

    }
}
