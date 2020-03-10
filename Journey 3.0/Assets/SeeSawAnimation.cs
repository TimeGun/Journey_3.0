using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeSawAnimation : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    
    private void OnTriggerEnter(Collider other)
    {
        _anim.SetBool("PlayerPresent", true);
    }

    private void OnTriggerExit(Collider other)
    {
        _anim.SetBool("PlayerPresent", false);
    }
}
