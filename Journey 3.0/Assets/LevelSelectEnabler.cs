using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectEnabler : MonoBehaviour
{
    public static LevelSelectEnabler instance;

    [SerializeField]private Button levelSelect;

    void Start()
    {
        instance = this;
    }

    public static void EnableButton()
    {
        instance.levelSelect.interactable = true;
    }
    
    public static void DisableButton()
    {
        instance.levelSelect.interactable = false;
    }
}
