using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public static CreditsManager instance;

    [SerializeField] private Animator _anim;

    private Coroutine coroutine;

    public bool creditsRunning = false;
    
    [SerializeField] private InputSetUp _inputSetUp;
    private InputMaster _controls;
    
    

    // Start is called before the first frame update
    void Start()
    {
        _controls = _inputSetUp.Controls;
        instance = this;
    }


    private void Update()
    {
        if (creditsRunning && _controls.PlayerFreeMovement.MenuBack.triggered)
        {
            StopCredits();
        }
    }

    void StopCredits()
    {
        creditsRunning = false;
        
        ProgressionData _resetData = new ProgressionData(0, false);
        
        SaveSystem.SaveProgress(_resetData);
        SceneManager.LoadScene("Manager Scene");
    }

    public static void StartCredits()
    {
        instance.coroutine = instance.StartCoroutine(instance.PlayCredits());
    }


    private IEnumerator PlayCredits()
    {
        creditsRunning = true;
        FadeToBlack.instance.SetBlack(true);
        
        yield return new WaitForSeconds(3f);

        
        AnimationClip[] clips = _anim.runtimeAnimatorController.animationClips;

        float clipLength = 0;

        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "ScrollText":
                    clipLength = clip.length;
                    break;
            }
        }

        _anim.Play("ScrollText");
        
        yield return new WaitForSeconds(clipLength);

        StopCredits();
    }
}
