using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLoadingBool : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    private void Awake()
    {
        SceneManagerScript.loadingUpdate += UpdateAnimatorLoadingBool;
    }

    void UpdateAnimatorLoadingBool(bool value)
    {
        _anim.SetBool("Loading", value);
    }

    private void OnDisable()
    {
        SceneManagerScript.loadingUpdate -= UpdateAnimatorLoadingBool;
    }
}
