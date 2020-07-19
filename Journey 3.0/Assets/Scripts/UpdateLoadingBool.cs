using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateLoadingBool : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    private void Awake()
    {
        SceneManagerScript.loadingUpdate += UpdateAnimatorLoadingBool;
    }

    void UpdateAnimatorLoadingBool(bool value)
    {
        if (SceneManager.sceneCount < 3)
        {
            _anim.SetBool("Loading", value);
        }
    }

    private void OnDisable()
    {
        SceneManagerScript.loadingUpdate -= UpdateAnimatorLoadingBool;
    }
}
