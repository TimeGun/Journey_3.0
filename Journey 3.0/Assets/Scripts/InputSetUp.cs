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

    

    private float _valueInteractDown;

    public float ValueInteractDown
    {
        get => _valueInteractDown;
        set => _valueInteractDown = value;
    }


    void Awake()
    {
        _controls = new InputMaster();

        _controls.PlayerFreeMovement.Movement.performed += ctx => _leftStick = ctx.ReadValue<Vector2>();
        _controls.PlayerFreeMovement.Movement.canceled += ctx => _leftStick = Vector2.zero;

        _controls.PlayerFreeMovement.InteractDown.performed += ctx => _valueInteractDown = ctx.ReadValue<float>();
        _controls.PlayerFreeMovement.InteractDown.canceled += ctx => _valueInteractDown = 0;
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
