// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""PlayerFreeMovement"",
            ""id"": ""51d49166-0491-4f16-b2d0-67e6ff49caa6"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f93fcb5e-1e4e-44cf-bf0e-1eedc12de3e0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""af0894bb-31ea-415d-a87e-70d6054f1095"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""8b48b0a2-890b-4b92-b5e4-8984f13b17aa"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""InteractDown"",
                    ""type"": ""Value"",
                    ""id"": ""f6d2e04d-d298-4b50-acf3-69f5a1e904d5"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""8cd2662a-cefe-4afb-96fd-81d6ac26cb22"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start Button"",
                    ""type"": ""Button"",
                    ""id"": ""5f35c8f4-a42b-4526-ac18-4bd4f6b43619"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""db189d30-95ce-4fb1-842c-c88b4eeddc08"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c41b52fa-353c-44d2-8e37-0cac8b427718"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""d41f5c86-ac78-4f77-a43a-b08966f6cbe3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e081f488-cb2a-4153-8e8c-2fd04d99a0fb"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1030af7b-be7d-45ee-963a-a9b05d0d2190"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6c981c4c-8663-4161-8316-1f01d57e3f78"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""05d2b174-fd49-45e1-8f4e-f279895d286d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f7991acf-ef8a-42b3-b2eb-29340b2d958c"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d2126e1-8a76-4010-bc50-b0febf839da7"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5173cd2-321a-4c2b-9ee1-badf74f93be4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63b44671-6b4c-4c9c-9002-5813c32e4a53"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""InteractDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50e3633f-06fd-4f90-b732-d54188df7e8c"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""InteractDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7ac35a5-e712-4bd7-b889-dca8d07443d8"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""29227b0c-e2d0-4e68-9738-7fddd360e5d8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1c4bfb07-8115-4f68-8f27-50390ec19ec9"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""47d57584-90f1-4d20-b6cf-acdade98e68e"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0caeacf0-92f7-471a-b113-0e14b29ed4d9"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""01b6b80d-f5f8-4ca0-9428-d7bc575a451c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e555f2f1-aa62-481f-acfb-4e6e6dc9436c"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Start Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5de18f03-8f07-47f3-a95e-a3b4ca343e71"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Start Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerFreeMovement
        m_PlayerFreeMovement = asset.FindActionMap("PlayerFreeMovement", throwIfNotFound: true);
        m_PlayerFreeMovement_Jump = m_PlayerFreeMovement.FindAction("Jump", throwIfNotFound: true);
        m_PlayerFreeMovement_Movement = m_PlayerFreeMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerFreeMovement_Interact = m_PlayerFreeMovement.FindAction("Interact", throwIfNotFound: true);
        m_PlayerFreeMovement_InteractDown = m_PlayerFreeMovement.FindAction("InteractDown", throwIfNotFound: true);
        m_PlayerFreeMovement_Aim = m_PlayerFreeMovement.FindAction("Aim", throwIfNotFound: true);
        m_PlayerFreeMovement_StartButton = m_PlayerFreeMovement.FindAction("Start Button", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerFreeMovement
    private readonly InputActionMap m_PlayerFreeMovement;
    private IPlayerFreeMovementActions m_PlayerFreeMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerFreeMovement_Jump;
    private readonly InputAction m_PlayerFreeMovement_Movement;
    private readonly InputAction m_PlayerFreeMovement_Interact;
    private readonly InputAction m_PlayerFreeMovement_InteractDown;
    private readonly InputAction m_PlayerFreeMovement_Aim;
    private readonly InputAction m_PlayerFreeMovement_StartButton;
    public struct PlayerFreeMovementActions
    {
        private @InputMaster m_Wrapper;
        public PlayerFreeMovementActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_PlayerFreeMovement_Jump;
        public InputAction @Movement => m_Wrapper.m_PlayerFreeMovement_Movement;
        public InputAction @Interact => m_Wrapper.m_PlayerFreeMovement_Interact;
        public InputAction @InteractDown => m_Wrapper.m_PlayerFreeMovement_InteractDown;
        public InputAction @Aim => m_Wrapper.m_PlayerFreeMovement_Aim;
        public InputAction @StartButton => m_Wrapper.m_PlayerFreeMovement_StartButton;
        public InputActionMap Get() { return m_Wrapper.m_PlayerFreeMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerFreeMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerFreeMovementActions instance)
        {
            if (m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnJump;
                @Movement.started -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnMovement;
                @Interact.started -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnInteract;
                @InteractDown.started -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnInteractDown;
                @InteractDown.performed -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnInteractDown;
                @InteractDown.canceled -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnInteractDown;
                @Aim.started -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnAim;
                @StartButton.started -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnStartButton;
                @StartButton.performed -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnStartButton;
                @StartButton.canceled -= m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface.OnStartButton;
            }
            m_Wrapper.m_PlayerFreeMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @InteractDown.started += instance.OnInteractDown;
                @InteractDown.performed += instance.OnInteractDown;
                @InteractDown.canceled += instance.OnInteractDown;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @StartButton.started += instance.OnStartButton;
                @StartButton.performed += instance.OnStartButton;
                @StartButton.canceled += instance.OnStartButton;
            }
        }
    }
    public PlayerFreeMovementActions @PlayerFreeMovement => new PlayerFreeMovementActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerFreeMovementActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnInteractDown(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnStartButton(InputAction.CallbackContext context);
    }
}
