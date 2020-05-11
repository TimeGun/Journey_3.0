using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressBToBack : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    
    [SerializeField] private InputSetUp _inputSetUp;
    private InputMaster _controls;

    private void Start()
    {
        _controls = _inputSetUp.Controls;
    }

    private void Update()
    {
        if (_controls.PlayerFreeMovement.MenuBack.triggered)
        {
            BackOut();
        }
    }

    void BackOut()
    {
        _backButton.onClick.Invoke();
    }
}
