using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateLoadingBool : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    public static UpdateLoadingBool instance;

    private void Awake()
    {
        instance = this;
        SceneManagerScript.loadingUpdate += UpdateAnimatorLoadingBool;
    }

    void UpdateAnimatorLoadingBool(bool value)
    {
        if (value)
        {
            if (SceneManager.sceneCount < 3)
            {
                _anim.SetBool("Loading", value);
            }
        }
        else
        {
            _anim.SetBool("Loading", value);
        }
        
    }

    private void OnDisable()
    {
        SceneManagerScript.loadingUpdate -= UpdateAnimatorLoadingBool;
    }

    public static void SaveGame()
    {
        instance._anim.SetTrigger("SavedGame");
    }
}
