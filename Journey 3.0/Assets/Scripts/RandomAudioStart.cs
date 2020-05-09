using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioStart : GradualLoader
{
    private AudioSource _source;
    // Start is called before the first frame update
    
    
    public override void Awake()
    {
        print("Called Awake");
        base.Awake();
    }
    
    
    IEnumerator Start()
    {
        yield return new WaitUntil(() => initialised);
        _source = GetComponent<AudioSource>();
        StartCoroutine(StartSound());
    }

    IEnumerator StartSound()
    {
        yield return new WaitForSeconds(Random.Range(0, 3f));
        
        _source.Play();
    }
    
    public override void EnqueThis()
    {
        base.EnqueThis();
    }

    public override void InitialiseThis()
    {
        base.InitialiseThis();
    }
}
