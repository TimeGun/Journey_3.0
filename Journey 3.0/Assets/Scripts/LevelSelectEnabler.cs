using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectEnabler : MonoBehaviour
{
    public static LevelSelectEnabler instance;


    [SerializeField] private Button levelSelect;

    void Start()
    {
        instance = this;
        levelSelect.interactable = LevelSelectSaveSystem.CheckGameFinished();
    }

    public static void EnableButton()
    {
        instance.levelSelect.interactable = LevelSelectSaveSystem.CheckGameFinished();
    }

    public static void DisableButton()
    {
        instance.levelSelect.interactable = false;
    }
}