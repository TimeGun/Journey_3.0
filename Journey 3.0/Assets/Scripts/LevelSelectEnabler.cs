using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectEnabler : MonoBehaviour
{
    public static LevelSelectEnabler instance;

    private bool offAtStart;

    [SerializeField] private Button levelSelect;

    void Start()
    {
        instance = this;
        offAtStart = !levelSelect.interactable;
    }

    public static void EnableButton()
    {
        if (!instance.offAtStart)
        {
            instance.levelSelect.interactable = LevelSelectSaveSystem.CheckGameFinished();
        }
    }
    
    public static void DisableButton()
    {
        if (!instance.offAtStart)
        {
            instance.levelSelect.interactable = false;
        }
    }
}
