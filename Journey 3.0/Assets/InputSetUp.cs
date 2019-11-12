using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSetUp : MonoBehaviour
{
    private InputMaster _controls;

    public InputMaster Controls
    {
        get => _controls;
    }

    private Vector2 _leftStick;

    public Vector2 LeftStick
    {
        get => _leftStick;
    }


    void Awake()
    {
        _controls = new InputMaster();

        _controls.PlayerFreeMovement.Movement.performed += ctx => _leftStick = ctx.ReadValue<Vector2>();
        _controls.PlayerFreeMovement.Movement.canceled += ctx => _leftStick = Vector2.zero;
    }

    
    void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}
