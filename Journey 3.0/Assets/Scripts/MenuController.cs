using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    [SerializeField] private GameObject baseMenuFirstButton, baseMenuControlsButton, baseMenuGalleryButton, baseMenuSettingsButton, baseMenuLevelButton, levelMenuFirstButton, controlsMenuFirstButton, settingsFirstButton, galleryFirstButton;

    [SerializeField] private GameObject baseMenu, settingsMenu, controlsMenu, galleryMenu, levelSelectMenu;

    public static MenuController instance;
    
    private InputMaster _controls;

    [SerializeField] private InputSetUp _inputSetUp;

    private bool gameStarted, inMenu;

    [SerializeField] private AudioSource _click, _select;

    public static bool formationUse;

    [SerializeField] private bool fadeAlpha = true;

    [SerializeField] private float alphaValue = 0.2f;

    private CanvasGroup baseGroup;
    
    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        baseGroup = baseMenu.GetComponent<CanvasGroup>();
        _controls = _inputSetUp.Controls;

        inMenu = true;
        gameStarted = false;

        _click.ignoreListenerPause = true;
        _select.ignoreListenerPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_controls.PlayerFreeMovement.StartButton.triggered || _controls.PlayerFreeMovement.MenuBack.triggered)
        {
            if (inMenu)
            {
                if (gameStarted && baseGroup.interactable)
                {
                    LeaveMenu();
                }
            }
            else if(_controls.PlayerFreeMovement.StartButton.triggered)
            {
                OpenPauseMenu();
            }
        }
    }


    public void OpenPauseMenu()
    {
        _anim.Play("OpenBase");
        
        if (gameStarted == true)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
        
        inMenu = true;
        API.GlobalReferences.PlayerRef.GetComponent<PlayerMovement>().DisableThis();
        baseMenu.SetActive(true);
        SetDefaultBaseSelected();
    }

    public void LeaveMenu()
    {
        _anim.Play("CloseBase");

        Time.timeScale = 1f;
        
        //baseMenu.SetActive(false);
        
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        galleryMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
        
        if (gameStarted == false && formationUse)
        {
            PlayOpeningCinematic.instance.CheckCinematic();
        }
        
        API.GlobalReferences.PlayerRef.GetComponent<PlayerMovement>().EnableThis();
        AudioListener.pause = false;
        inMenu = false;
        
        baseMenuFirstButton.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
        
        gameStarted = true;
    }

    

    public void SetDefaultBaseSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(baseMenuFirstButton);
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
    
    public void SetDefaultLevelSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(levelMenuFirstButton);
    }

    public void SetLevelBaseSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(baseMenuLevelButton);
    }
    
    public void SetGalleryBaseSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(baseMenuGalleryButton);
    }
    
    public void SetDefaultGallerySelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(galleryFirstButton);
    }

    public void LoadStartZone()
    {
        StartCoroutine(ChangeZone(0, false));
    }
    public void LoadBridgeZone()
    {
        StartCoroutine(ChangeZone(1, false));
    }
    public void LoadPillarZone()
    {
        StartCoroutine(ChangeZone(2, true));
    }
    public void LoadPaintingZone()
    {
        StartCoroutine(ChangeZone(3, true));
    }

    IEnumerator ChangeZone(int zone, bool night)
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        gameStarted = false;
        API.GlobalReferences.PlayerRef.GetComponent<ObjectDetection>().ClearList();
        API.GlobalReferences.PlayerRef.GetComponent<InteractWithObject>().StopInteracting();
        FadeToBlack.instance.SetBlack(true);
        AmbienceManager.FadeOutMasterSound();
        yield return new WaitForSeconds(1f);


        
        API.GlobalReferences.PlayerRef.GetComponent<InteractWithObject>().StopInteracting();
        


        ProgressionData _newProgressionData = new ProgressionData(zone, night);
        SaveSystem.SaveProgress(_newProgressionData);
        SceneManagerScript.instance.StopAllCoroutines();
        API.GlobalReferences.PlayerRef.GetComponent<InteractWithObject>().StopInteracting();
        yield return new WaitForEndOfFrame();
        baseMenuFirstButton.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
        SceneManagerScript.instance.StartCoroutine(SceneManagerScript.instance.StartGameLoad(_newProgressionData));
    }

    public void OpenSubMenu(GameObject subMenuToOpen)
    {
        _anim.Play("OpenSub");
        subMenuToOpen.SetActive(true);
        
        CanvasGroup baseGroup = baseMenu.GetComponent<CanvasGroup>();
        
        baseGroup.interactable = false;
        
        if(fadeAlpha)
            baseGroup.alpha = alphaValue;
    }

    public void CloseSubMenu()
    {
        _anim.Play("CloseSub");

        baseGroup.interactable = true;
        
        if(fadeAlpha)
            baseGroup.alpha = 1f;
    }


    public void TurnOffSubMenues()
    {
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        galleryMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
    }

    public void CloseBaseMenu()
    {
        _anim.Play("CloseBase");
    }

    public void QuitGame()
     {
         // save any game data here
         #if UNITY_EDITOR
             // Application.Quit() does not work in the editor so
             // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
             UnityEditor.EditorApplication.isPlaying = false;
         #else
             Application.Quit();
         #endif
     }
}
