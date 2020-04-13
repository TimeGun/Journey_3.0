using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class  InputSetUp : MonoBehaviour
{
    private InputMaster _controls;

    public string lastInput;

    private InputUser _inputUser;
    

    public InputMaster Controls
    {
        get => _controls;
    }

    private Vector2 _leftStick;

    public Vector2 LeftStick
    {
        get => _leftStick;
    }

    public Vector2 _rightStick;

    public Vector2 RightStick
    {
        get => _rightStick;
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

        _controls.PlayerFreeMovement.Aim.performed += ctx => _rightStick = ctx.ReadValue<Vector2>();
        _controls.PlayerFreeMovement.Aim.canceled += ctx => _rightStick = Vector2.zero;

        _controls.PlayerFreeMovement.InteractDown.performed += ctx => _valueInteractDown = ctx.ReadValue<float>();
        _controls.PlayerFreeMovement.InteractDown.canceled += ctx => _valueInteractDown = 0;

        PlayerInput input = GetComponent<PlayerInput>();

        lastInput = input.currentControlScheme;

    }


    void onInputDeviceChange(InputUser user, InputUserChange change, InputDevice device) {
        if (change == InputUserChange.ControlSchemeChanged) {
            lastInput = user.controlScheme.Value.name;
        }
    }



    void OnEnable()
    {
        _controls.Enable();
        InputUser.onChange += onInputDeviceChange;
    }

    private void OnDisable()
    {
        _controls.Disable();
        InputUser.onChange -= onInputDeviceChange;
    }
}
