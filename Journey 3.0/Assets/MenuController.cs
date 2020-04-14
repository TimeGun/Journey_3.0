using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    [SerializeField] private GameObject baseMenuFirstButton, baseMenuControlsButton, baseMenuSettingsButton, controlsMenuFirstButton, settingsFirstButton;

    public static MenuController instance;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void FadeInMenu()
    {
        instance._anim.SetTrigger("FadeIn");
    }

    public void SetDefaultControlsSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsMenuFirstButton);
    }

    public void SetDefaultSettingsSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingsFirstButton);
    }

    public void SetControlsBaseSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(baseMenuControlsButton);
    }
    
    public void SetSettingsBaseSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(baseMenuSettingsButton);
    }
}
