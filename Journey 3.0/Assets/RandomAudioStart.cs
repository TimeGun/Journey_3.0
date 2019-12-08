using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioStart : MonoBehaviour
{
    private AudioSource _source;
    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
        StartCoroutine(StartSound());
    }

    IEnumerator StartSound()
    {
        yield return new WaitForSeconds(Random.Range(0, 3f));
        
        _source.Play();
    }
}
