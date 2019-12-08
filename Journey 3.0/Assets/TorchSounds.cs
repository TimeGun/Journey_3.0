using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchSounds : MonoBehaviour
{
    private TorchOnOff _torchOnOff;

    private Coroutine _coroutine;

    private AudioSource _source;

    public AudioClip[] _clips;
    
    // Start is called before the first frame update
    void Start()
    {
        _torchOnOff = GetComponent<TorchOnOff>();
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_torchOnOff.isLit && _coroutine == null)
        {
            _coroutine = StartCoroutine(TorchSoundsCoroutine());
        }
    }


    private IEnumerator TorchSoundsCoroutine()
    {
        _source.loop = false;
        _source.clip = _clips[0];
        
        _source.Play();

        yield return new WaitUntil(() => _source.isPlaying == false);
        
        _source.loop = true;
        _source.clip = _clips[1];
        
        _source.Play();
        
        while (true)
        {
            if (_torchOnOff.isLit == false)
            {
                _source.Stop();
                StopAllCoroutines();
                _coroutine = null;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
