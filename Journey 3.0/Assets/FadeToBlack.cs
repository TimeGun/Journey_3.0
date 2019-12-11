using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    public static FadeToBlack instance;
    
    private void Awake()
    {
        instance = this;
    }

    public void SetBars(bool value)
    {
        _anim.SetBool("blackBarsOn", value);
    }
}
