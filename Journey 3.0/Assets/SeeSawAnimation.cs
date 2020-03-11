using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeSawAnimation : MonoBehaviour
{
    [SerializeField] private Animator _anim;


    public void SetAnimationBoolS(string boolName, bool value)
    {
        _anim.SetBool(boolName, value);
    }
}
