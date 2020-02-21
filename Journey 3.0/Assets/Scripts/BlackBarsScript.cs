using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBarsScript : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    public static BlackBarsScript instance;
    
    private void Awake()
    {
        instance = this;
    }

    public void SetBars(bool value)
    {
        _anim.SetBool("blackBarsOn", value);
    }
}
