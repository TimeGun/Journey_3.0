using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenForPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _topRunePart;

    private GameObject player;

    [SerializeField] private float _hoverDistance = 2f;
    [SerializeField] [Range(0, 1)] private float _minimumOpeningPercentage;

    [SerializeField] private float _activationDistance = 5f;

    private float _lerpSpeed;
    
    [SerializeField] private float _lerpSpeedMax;
    [SerializeField] [Range(0, 1)] private float _minimumLerpSpeedPercentage;

    [SerializeField] private bool testing;

    [SerializeField] private float _slamSpeed;

    private Vector3 _closedRunePosition;

    void Start()
    {
        if (testing)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            player = API.GlobalReferences.PlayerRef;
        }

        _closedRunePosition = _topRunePart.transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer < _activationDistance)
        {
            _lerpSpeed = Map(distanceToPlayer, _activationDistance, 0, _lerpSpeedMax * _minimumLerpSpeedPercentage, _lerpSpeedMax);
            float tempHoverDistance = Map(distanceToPlayer, _activationDistance, 0, _hoverDistance * _minimumOpeningPercentage, _hoverDistance);
            _topRunePart.transform.position = 
                Vector3.Lerp(_topRunePart.transform.position, _closedRunePosition + new Vector3(0, tempHoverDistance, 0), Time.deltaTime * _lerpSpeed);
        }
        else
        {
            _lerpSpeed = _lerpSpeedMax;
            _topRunePart.transform.position = Vector3.MoveTowards(_topRunePart.transform.position, _closedRunePosition, Time.deltaTime * _slamSpeed);
        }
    }
    
    public float Map(float a, float b, float c, float d, float e)
    {
        
        float cb = c - b;
        float de = e - d;
        float howFar = (a - b) / cb;
        return d + howFar * de;
        //float a = value you want mapped t
    }
}
