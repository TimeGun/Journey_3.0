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

    private bool _playerActivated = false;

    private float _itemPresentHeightOffset;

    public float ItemPresentHeightOffset
    {
        get => _itemPresentHeightOffset;
        set => _itemPresentHeightOffset = value;
    }

    [SerializeField] private HoldInteractipleOnRune _holdInteractipleOnRune;

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
            _playerActivated = true;
            
            if (_holdInteractipleOnRune.ItemOnRuneBool)
            {
                _lerpSpeed = Map(distanceToPlayer, _activationDistance, 0, _lerpSpeedMax * _minimumLerpSpeedPercentage, _lerpSpeedMax);
                float tempHoverDistance = Map(distanceToPlayer, _activationDistance, 0, _hoverDistance * _minimumOpeningPercentage, _hoverDistance);
                _topRunePart.transform.position = 
                    Vector3.Lerp(_topRunePart.transform.position, _closedRunePosition + new Vector3(0, _hoverDistance, 0), Time.deltaTime * _lerpSpeed);
            }
            else
            {
                _lerpSpeed = Map(distanceToPlayer, _activationDistance, 0, _lerpSpeedMax * _minimumLerpSpeedPercentage, _lerpSpeedMax);
                float tempHoverDistance = Map(distanceToPlayer, _activationDistance, 0, _hoverDistance * _minimumOpeningPercentage, _hoverDistance);
                _topRunePart.transform.position = 
                    Vector3.Lerp(_topRunePart.transform.position, _closedRunePosition + new Vector3(0, tempHoverDistance, 0), Time.deltaTime * _lerpSpeed);
            }
        }
        else
        {
            Vector3 heightOffsetVector = new Vector3(0, _itemPresentHeightOffset, 0);
            
            if (_playerActivated && _topRunePart.transform.position == _closedRunePosition + heightOffsetVector)
            {
                StartCoroutine(SlamDown());
                _playerActivated = false;
            }

            _lerpSpeed = _lerpSpeedMax;
            _topRunePart.transform.position = Vector3.MoveTowards(_topRunePart.transform.position, _closedRunePosition + heightOffsetVector, Time.deltaTime * _slamSpeed);
        }
    }

    IEnumerator SlamDown()
    {
        print("Slammed down");

        if (_holdInteractipleOnRune.ItemOnRuneBool)
        {
            SquishObject item = _holdInteractipleOnRune.ItemOnRune.GetComponent<SquishObject>();
            
            if (item != null)
            {
                item.Squish();
            }
        }

        //check where to move rune down to
        //call squishObject
        //spawn particles (other flair)
        yield return null;
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
