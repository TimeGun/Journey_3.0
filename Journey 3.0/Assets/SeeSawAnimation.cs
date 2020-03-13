using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeSawAnimation : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    [SerializeField] private SeeSawPlacement _seeSawPlacement;


    public void SetAnimationBoolS(string boolName, bool value)
    {
        _anim.SetBool(boolName, value);
    }


    public void LaunchRock()
    {
        print("test");
    }
}
