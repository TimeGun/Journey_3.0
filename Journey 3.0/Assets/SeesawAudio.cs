using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawAudio : MonoBehaviour
{

    [SerializeField] private AudioSource _creakSound;

    [SerializeField] private Animator _animator;

    bool tipped;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Creak"))
        {
            if (!_creakSound.isPlaying)
            {
                _creakSound.PlayOneShot(_creakSound.clip);
            }
        }
        else
        {
            _creakSound.Stop();
        }
    }
}
