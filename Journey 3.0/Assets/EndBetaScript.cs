using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndBetaScript : MonoBehaviour
{
    [SerializeField] private GameObject steam;

    [SerializeField] private MenuController _menuController;

    public void EnableBateScreen()
    {
        _menuController.enabled = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(steam);
    }
}