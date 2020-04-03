using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitationSound : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    [SerializeField] private OpenForPlayer _openForPlayer;

    [SerializeField] private float minVolume;
    [SerializeField] private float maxVolume;

    [SerializeField] private float lerpSpeed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_openForPlayer.open)
        {
            _source.volume = Mathf.Lerp(_source.volume, maxVolume, Time.deltaTime * lerpSpeed);
        }
        else
        {
            _source.volume = Mathf.Lerp(_source.volume, minVolume, Time.deltaTime * lerpSpeed);
        }
    }
}
