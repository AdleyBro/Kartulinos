// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Kart/InputManager.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputManager : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputManager()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputManager"",
    ""maps"": [
        {
            ""name"": ""Player1"",
            ""id"": ""aa288812-bd35-4943-b614-7602aa181994"",
            ""actions"": [
                {
                    ""name"": ""Steering"",
                    ""type"": ""Value"",
                    ""id"": ""f4fe4a30-8575-4684-8aba-2cf6b56d62c6"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Accelerating"",
                    ""type"": ""Value"",
                    ""id"": ""0e9c935c-ac2e-4393-bb9c-532013249a0f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Regressing"",
                    ""type"": ""Value"",
                    ""id"": ""8c086c45-c7e2-45e7-8ce3-a4c51f9b6612"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookBack"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5d42e107-f10f-4bb6-96c2-a6374ed97c07"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drifting"",
                    ""type"": ""Value"",
                    ""id"": ""5e4bffa8-3648-4807-8824-977dc5f938ab"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UsingItem"",
                    ""type"": ""Value"",
                    ""id"": ""0e326134-aaba-46a4-ac31-4eebb0752aae"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GetItem"",
                    ""type"": ""Button"",
                    ""id"": ""8879691f-e304-4d91-8112-af8888d90f6f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftJoyStick"",
                    ""type"": ""Value"",
                    ""id"": ""89d02c88-3b21-4e17-9134-636a5e59fa56"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Teleport"",
                    ""type"": ""Button"",
                    ""id"": ""ba9d138d-6372-4d5b-bc67-c4b5356b0bfa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d843f07e-abd8-4477-ac8f-3d0324f3f6b0"",
                    ""path"": ""<XInputController>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14cb4853-466b-4bdb-b4fd-1e92d99f49a8"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0018d13-0498-4e84-81ba-32861b281d59"",
                    ""path"": ""<Joystick>/stick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce88c88c-533f-4fa4-9c7e-d454c78baf8e"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerating"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3820193a-e888-40ef-93ae-944e1aafd623"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerating"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1c6e63a-0c85-409d-9737-447022a7a9be"",
                    ""path"": ""<HID::MY-POWER CO.,LTD. USB Joystick>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerating"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c43e9cf3-7f3a-4d71-98e3-97dc9ee1d4a4"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Regressing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8515d0c2-076c-4dac-9895-c8a15c084440"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Regressing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3be24499-ed00-4622-96f3-0a29fd0df7ff"",
                    ""path"": ""<HID::MY-POWER CO.,LTD. USB Joystick>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Regressing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""527a2354-5c49-4c94-b763-0f86dc5ac4c8"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""539d337b-b711-4472-b3c4-9aabd4a6b40a"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e20abe8-4324-4af0-811d-b60139978112"",
                    ""path"": ""<HID::MY-POWER CO.,LTD. USB Joystick>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8d8d6a1-fccc-43e6-bd41-36cc07895861"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drifting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d8a3553-385a-410c-8891-231d5efda3dd"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drifting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb68d975-d7b0-439d-ae41-9961c53f31c0"",
                    ""path"": ""<HID::MY-POWER CO.,LTD. USB Joystick>/button6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drifting"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6438e15-f477-4d80-8063-a14a04d6504b"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsingItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""821bb444-e4e9-43c3-ae72-1e8c868517e6"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsingItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0c1730d-ce43-4461-bd85-39259d4b4807"",
                    ""path"": ""<HID::MY-POWER CO.,LTD. USB Joystick>/button5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsingItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d3e3a4e-47b7-42c2-853e-51652e205bf6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GetItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e764eb1-a845-40df-afef-95a10aef6fa7"",
                    ""path"": ""<Gamepad>/leftStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftJoyStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""22bbca3c-86b8-409e-9812-cd0d56d3ffc2"",
                    ""path"": ""<XInputController>/leftStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftJoyStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48f3f2fe-d90b-4f8b-ba1b-72013f1a8886"",
                    ""path"": ""<Joystick>/stick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftJoyStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57f42824-c32f-4002-842d-bdc44d510823"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Teleport"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Test"",
            ""bindingGroup"": ""Test"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Player2"",
            ""bindingGroup"": ""Player2"",
            ""devices"": [
                {
                    ""devicePath"": ""<HID::MY-POWER CO.,LTD. USB Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player1
        m_Player1 = asset.FindActionMap("Player1", throwIfNotFound: true);
        m_Player1_Steering = m_Player1.FindAction("Steering", throwIfNotFound: true);
        m_Player1_Accelerating = m_Player1.FindAction("Accelerating", throwIfNotFound: true);
        m_Player1_Regressing = m_Player1.FindAction("Regressing", throwIfNotFound: true);
        m_Player1_LookBack = m_Player1.FindAction("LookBack", throwIfNotFound: true);
        m_Player1_Drifting = m_Player1.FindAction("Drifting", throwIfNotFound: true);
        m_Player1_UsingItem = m_Player1.FindAction("UsingItem", throwIfNotFound: true);
        m_Player1_GetItem = m_Player1.FindAction("GetItem", throwIfNotFound: true);
        m_Player1_LeftJoyStick = m_Player1.FindAction("LeftJoyStick", throwIfNotFound: true);
        m_Player1_Teleport = m_Player1.FindAction("Teleport", throwIfNotFound: true);
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

    // Player1
    private readonly InputActionMap m_Player1;
    private IPlayer1Actions m_Player1ActionsCallbackInterface;
    private readonly InputAction m_Player1_Steering;
    private readonly InputAction m_Player1_Accelerating;
    private readonly InputAction m_Player1_Regressing;
    private readonly InputAction m_Player1_LookBack;
    private readonly InputAction m_Player1_Drifting;
    private readonly InputAction m_Player1_UsingItem;
    private readonly InputAction m_Player1_GetItem;
    private readonly InputAction m_Player1_LeftJoyStick;
    private readonly InputAction m_Player1_Teleport;
    public struct Player1Actions
    {
        private @InputManager m_Wrapper;
        public Player1Actions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Steering => m_Wrapper.m_Player1_Steering;
        public InputAction @Accelerating => m_Wrapper.m_Player1_Accelerating;
        public InputAction @Regressing => m_Wrapper.m_Player1_Regressing;
        public InputAction @LookBack => m_Wrapper.m_Player1_LookBack;
        public InputAction @Drifting => m_Wrapper.m_Player1_Drifting;
        public InputAction @UsingItem => m_Wrapper.m_Player1_UsingItem;
        public InputAction @GetItem => m_Wrapper.m_Player1_GetItem;
        public InputAction @LeftJoyStick => m_Wrapper.m_Player1_LeftJoyStick;
        public InputAction @Teleport => m_Wrapper.m_Player1_Teleport;
        public InputActionMap Get() { return m_Wrapper.m_Player1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player1Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer1Actions instance)
        {
            if (m_Wrapper.m_Player1ActionsCallbackInterface != null)
            {
                @Steering.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSteering;
                @Steering.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSteering;
                @Steering.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnSteering;
                @Accelerating.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAccelerating;
                @Accelerating.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAccelerating;
                @Accelerating.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnAccelerating;
                @Regressing.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnRegressing;
                @Regressing.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnRegressing;
                @Regressing.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnRegressing;
                @LookBack.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnLookBack;
                @LookBack.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnLookBack;
                @LookBack.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnLookBack;
                @Drifting.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnDrifting;
                @Drifting.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnDrifting;
                @Drifting.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnDrifting;
                @UsingItem.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnUsingItem;
                @UsingItem.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnUsingItem;
                @UsingItem.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnUsingItem;
                @GetItem.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnGetItem;
                @GetItem.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnGetItem;
                @GetItem.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnGetItem;
                @LeftJoyStick.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnLeftJoyStick;
                @LeftJoyStick.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnLeftJoyStick;
                @LeftJoyStick.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnLeftJoyStick;
                @Teleport.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnTeleport;
                @Teleport.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnTeleport;
                @Teleport.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnTeleport;
            }
            m_Wrapper.m_Player1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Steering.started += instance.OnSteering;
                @Steering.performed += instance.OnSteering;
                @Steering.canceled += instance.OnSteering;
                @Accelerating.started += instance.OnAccelerating;
                @Accelerating.performed += instance.OnAccelerating;
                @Accelerating.canceled += instance.OnAccelerating;
                @Regressing.started += instance.OnRegressing;
                @Regressing.performed += instance.OnRegressing;
                @Regressing.canceled += instance.OnRegressing;
                @LookBack.started += instance.OnLookBack;
                @LookBack.performed += instance.OnLookBack;
                @LookBack.canceled += instance.OnLookBack;
                @Drifting.started += instance.OnDrifting;
                @Drifting.performed += instance.OnDrifting;
                @Drifting.canceled += instance.OnDrifting;
                @UsingItem.started += instance.OnUsingItem;
                @UsingItem.performed += instance.OnUsingItem;
                @UsingItem.canceled += instance.OnUsingItem;
                @GetItem.started += instance.OnGetItem;
                @GetItem.performed += instance.OnGetItem;
                @GetItem.canceled += instance.OnGetItem;
                @LeftJoyStick.started += instance.OnLeftJoyStick;
                @LeftJoyStick.performed += instance.OnLeftJoyStick;
                @LeftJoyStick.canceled += instance.OnLeftJoyStick;
                @Teleport.started += instance.OnTeleport;
                @Teleport.performed += instance.OnTeleport;
                @Teleport.canceled += instance.OnTeleport;
            }
        }
    }
    public Player1Actions @Player1 => new Player1Actions(this);
    private int m_TestSchemeIndex = -1;
    public InputControlScheme TestScheme
    {
        get
        {
            if (m_TestSchemeIndex == -1) m_TestSchemeIndex = asset.FindControlSchemeIndex("Test");
            return asset.controlSchemes[m_TestSchemeIndex];
        }
    }
    private int m_Player2SchemeIndex = -1;
    public InputControlScheme Player2Scheme
    {
        get
        {
            if (m_Player2SchemeIndex == -1) m_Player2SchemeIndex = asset.FindControlSchemeIndex("Player2");
            return asset.controlSchemes[m_Player2SchemeIndex];
        }
    }
    public interface IPlayer1Actions
    {
        void OnSteering(InputAction.CallbackContext context);
        void OnAccelerating(InputAction.CallbackContext context);
        void OnRegressing(InputAction.CallbackContext context);
        void OnLookBack(InputAction.CallbackContext context);
        void OnDrifting(InputAction.CallbackContext context);
        void OnUsingItem(InputAction.CallbackContext context);
        void OnGetItem(InputAction.CallbackContext context);
        void OnLeftJoyStick(InputAction.CallbackContext context);
        void OnTeleport(InputAction.CallbackContext context);
    }
}
