//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Editors/PlayerController/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""62fc55c1-a5b4-49e2-bafa-ec479aa90d78"",
            ""actions"": [
                {
                    ""name"": ""Movement_Player"",
                    ""type"": ""Value"",
                    ""id"": ""5ed400fe-779a-4b0a-9a14-0f4f22cec4c9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Movement_Bras"",
                    ""type"": ""Value"",
                    ""id"": ""d72052e4-f5c2-4ddc-9b87-c535547fc641"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Torch"",
                    ""type"": ""Button"",
                    ""id"": ""eb6f13d1-86b3-4fc2-bd01-a7fc8e1ed417"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""UI"",
                    ""type"": ""Button"",
                    ""id"": ""36504d76-f7fa-4840-9ddd-59afe9dcdcc2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Turbo"",
                    ""type"": ""Button"",
                    ""id"": ""c3329b60-badd-49a4-8c30-2b7fd2f97852"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""c1f10245-c769-4dc2-b571-dbda48d6558c"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement_Bras"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a82a8f31-d45e-4c82-b75d-62e0557dbfa4"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement_Bras"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""02d3654d-5817-4aae-a96b-219bc54f72e9"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement_Bras"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d82881af-ef0f-400e-91bd-5f065d8a7531"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement_Bras"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""71c3ba96-3dc7-4ad5-a809-73e4d98b7207"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement_Bras"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""ba608869-a45f-4038-8b61-b0e909158ffd"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2"",
                    ""groups"": """",
                    ""action"": ""Movement_Player"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""01e18f4a-4115-4362-87d5-3ea4b13123c6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement_Player"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""85ce53e7-8342-4c3c-9151-c1b30d3065bc"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement_Player"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""09a68fb9-93ba-4f86-8886-997ad1b2388e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement_Player"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5cb7865e-84ee-4c73-8783-bc74aa6ca51a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement_Player"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""49757db9-9420-412b-91d7-a69cd973b911"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Torch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7dfea958-8104-4c8d-982d-cf6cac5efa4c"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b1322dc-c545-4b3c-a025-546d5ed8f511"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turbo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement_Player = m_Player.FindAction("Movement_Player", throwIfNotFound: true);
        m_Player_Movement_Bras = m_Player.FindAction("Movement_Bras", throwIfNotFound: true);
        m_Player_Torch = m_Player.FindAction("Torch", throwIfNotFound: true);
        m_Player_UI = m_Player.FindAction("UI", throwIfNotFound: true);
        m_Player_Turbo = m_Player.FindAction("Turbo", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement_Player;
    private readonly InputAction m_Player_Movement_Bras;
    private readonly InputAction m_Player_Torch;
    private readonly InputAction m_Player_UI;
    private readonly InputAction m_Player_Turbo;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement_Player => m_Wrapper.m_Player_Movement_Player;
        public InputAction @Movement_Bras => m_Wrapper.m_Player_Movement_Bras;
        public InputAction @Torch => m_Wrapper.m_Player_Torch;
        public InputAction @UI => m_Wrapper.m_Player_UI;
        public InputAction @Turbo => m_Wrapper.m_Player_Turbo;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement_Player.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement_Player;
                @Movement_Player.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement_Player;
                @Movement_Player.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement_Player;
                @Movement_Bras.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement_Bras;
                @Movement_Bras.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement_Bras;
                @Movement_Bras.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement_Bras;
                @Torch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTorch;
                @Torch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTorch;
                @Torch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTorch;
                @UI.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUI;
                @UI.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUI;
                @UI.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUI;
                @Turbo.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurbo;
                @Turbo.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurbo;
                @Turbo.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurbo;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement_Player.started += instance.OnMovement_Player;
                @Movement_Player.performed += instance.OnMovement_Player;
                @Movement_Player.canceled += instance.OnMovement_Player;
                @Movement_Bras.started += instance.OnMovement_Bras;
                @Movement_Bras.performed += instance.OnMovement_Bras;
                @Movement_Bras.canceled += instance.OnMovement_Bras;
                @Torch.started += instance.OnTorch;
                @Torch.performed += instance.OnTorch;
                @Torch.canceled += instance.OnTorch;
                @UI.started += instance.OnUI;
                @UI.performed += instance.OnUI;
                @UI.canceled += instance.OnUI;
                @Turbo.started += instance.OnTurbo;
                @Turbo.performed += instance.OnTurbo;
                @Turbo.canceled += instance.OnTurbo;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement_Player(InputAction.CallbackContext context);
        void OnMovement_Bras(InputAction.CallbackContext context);
        void OnTorch(InputAction.CallbackContext context);
        void OnUI(InputAction.CallbackContext context);
        void OnTurbo(InputAction.CallbackContext context);
    }
}
