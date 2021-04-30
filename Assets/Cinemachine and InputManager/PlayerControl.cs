// GENERATED AUTOMATICALLY FROM 'Assets/Cinemachine and InputManager/PlayerControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControl"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""c1b72f49-4476-4dc2-a67d-c00a8432e586"",
            ""actions"": [
                {
                    ""name"": ""MouseMovement"",
                    ""type"": ""Button"",
                    ""id"": ""b683d0aa-f171-448e-8015-89f8e223e788"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PlayerMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4164eec3-0dab-4d41-b021-542b98fd7a56"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchCameras"",
                    ""type"": ""Button"",
                    ""id"": ""6e4cff68-5239-4484-828a-ad41d1df77d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5700adf5-d484-4bc2-b5f1-d538fb20f770"",
                    ""expectedControlType"": ""Key"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""1e72d654-26a4-448f-bdb6-d5bf207dc638"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""StartPlacingItem"",
                    ""type"": ""Button"",
                    ""id"": ""bac57f43-8b4a-4895-a25f-5eec12e87e33"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""841f2327-5a8a-425a-aec1-6ec4bfc5048a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UnlockCursor"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5eb3a723-17f3-410c-b9aa-78b75211946a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseRightClick"",
                    ""type"": ""Button"",
                    ""id"": ""963d1b6d-7505-4af5-ab1d-99cfba9027a8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c7eab57a-9c02-4237-8633-0c70f5c02f54"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""762feebc-dce6-45df-aaef-93630e265e53"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WSAD"",
                    ""id"": ""c03240e9-6d28-4fdf-83f0-38d2a248e05c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a2600f1e-7000-4fe6-a65d-173532043e31"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e8ec7e5d-35c0-4ff1-83d0-bef0096de125"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2cec695b-1caf-44f4-862f-6c653cce03e2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d3359fd3-4add-4409-b5f7-6cb7cca5d3b8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""055a7b21-fd46-4c8a-b960-49856b33406a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchCameras"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Axis"",
                    ""id"": ""8bc16955-4cc0-4c1c-89d4-be4fd6182766"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f1980c20-348f-485a-b9a4-e23da8d87c17"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""353ff307-e249-4382-9d9b-4b177b18efef"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""38a9d060-8960-4b57-8cd7-8b23a34577f4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7bcae382-3b46-4a51-ba49-267e5709a9af"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""StartPlacingItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ed837e0-535a-49e5-8738-b7c751d8a59f"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b31eb65c-a631-40e1-b267-9199e332dd01"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UnlockCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9350c25d-99bf-42e8-9296-702cc42390e1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseRightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a514b433-8a8e-440b-8705-56ce13861372"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseX"",
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
        m_Player_MouseMovement = m_Player.FindAction("MouseMovement", throwIfNotFound: true);
        m_Player_PlayerMovement = m_Player.FindAction("PlayerMovement", throwIfNotFound: true);
        m_Player_SwitchCameras = m_Player.FindAction("SwitchCameras", throwIfNotFound: true);
        m_Player_Rotation = m_Player.FindAction("Rotation", throwIfNotFound: true);
        m_Player_MouseClick = m_Player.FindAction("MouseClick", throwIfNotFound: true);
        m_Player_StartPlacingItem = m_Player.FindAction("StartPlacingItem", throwIfNotFound: true);
        m_Player_MousePosition = m_Player.FindAction("MousePosition", throwIfNotFound: true);
        m_Player_UnlockCursor = m_Player.FindAction("UnlockCursor", throwIfNotFound: true);
        m_Player_MouseRightClick = m_Player.FindAction("MouseRightClick", throwIfNotFound: true);
        m_Player_MouseX = m_Player.FindAction("MouseX", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_MouseMovement;
    private readonly InputAction m_Player_PlayerMovement;
    private readonly InputAction m_Player_SwitchCameras;
    private readonly InputAction m_Player_Rotation;
    private readonly InputAction m_Player_MouseClick;
    private readonly InputAction m_Player_StartPlacingItem;
    private readonly InputAction m_Player_MousePosition;
    private readonly InputAction m_Player_UnlockCursor;
    private readonly InputAction m_Player_MouseRightClick;
    private readonly InputAction m_Player_MouseX;
    public struct PlayerActions
    {
        private @PlayerControl m_Wrapper;
        public PlayerActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseMovement => m_Wrapper.m_Player_MouseMovement;
        public InputAction @PlayerMovement => m_Wrapper.m_Player_PlayerMovement;
        public InputAction @SwitchCameras => m_Wrapper.m_Player_SwitchCameras;
        public InputAction @Rotation => m_Wrapper.m_Player_Rotation;
        public InputAction @MouseClick => m_Wrapper.m_Player_MouseClick;
        public InputAction @StartPlacingItem => m_Wrapper.m_Player_StartPlacingItem;
        public InputAction @MousePosition => m_Wrapper.m_Player_MousePosition;
        public InputAction @UnlockCursor => m_Wrapper.m_Player_UnlockCursor;
        public InputAction @MouseRightClick => m_Wrapper.m_Player_MouseRightClick;
        public InputAction @MouseX => m_Wrapper.m_Player_MouseX;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @MouseMovement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseMovement;
                @MouseMovement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseMovement;
                @MouseMovement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseMovement;
                @PlayerMovement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayerMovement;
                @PlayerMovement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayerMovement;
                @PlayerMovement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPlayerMovement;
                @SwitchCameras.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchCameras;
                @SwitchCameras.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchCameras;
                @SwitchCameras.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchCameras;
                @Rotation.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotation;
                @MouseClick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseClick;
                @MouseClick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseClick;
                @MouseClick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseClick;
                @StartPlacingItem.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStartPlacingItem;
                @StartPlacingItem.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStartPlacingItem;
                @StartPlacingItem.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStartPlacingItem;
                @MousePosition.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMousePosition;
                @UnlockCursor.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUnlockCursor;
                @UnlockCursor.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUnlockCursor;
                @UnlockCursor.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUnlockCursor;
                @MouseRightClick.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseRightClick;
                @MouseRightClick.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseRightClick;
                @MouseRightClick.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseRightClick;
                @MouseX.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseX;
                @MouseX.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseX;
                @MouseX.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMouseX;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseMovement.started += instance.OnMouseMovement;
                @MouseMovement.performed += instance.OnMouseMovement;
                @MouseMovement.canceled += instance.OnMouseMovement;
                @PlayerMovement.started += instance.OnPlayerMovement;
                @PlayerMovement.performed += instance.OnPlayerMovement;
                @PlayerMovement.canceled += instance.OnPlayerMovement;
                @SwitchCameras.started += instance.OnSwitchCameras;
                @SwitchCameras.performed += instance.OnSwitchCameras;
                @SwitchCameras.canceled += instance.OnSwitchCameras;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
                @MouseClick.started += instance.OnMouseClick;
                @MouseClick.performed += instance.OnMouseClick;
                @MouseClick.canceled += instance.OnMouseClick;
                @StartPlacingItem.started += instance.OnStartPlacingItem;
                @StartPlacingItem.performed += instance.OnStartPlacingItem;
                @StartPlacingItem.canceled += instance.OnStartPlacingItem;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @UnlockCursor.started += instance.OnUnlockCursor;
                @UnlockCursor.performed += instance.OnUnlockCursor;
                @UnlockCursor.canceled += instance.OnUnlockCursor;
                @MouseRightClick.started += instance.OnMouseRightClick;
                @MouseRightClick.performed += instance.OnMouseRightClick;
                @MouseRightClick.canceled += instance.OnMouseRightClick;
                @MouseX.started += instance.OnMouseX;
                @MouseX.performed += instance.OnMouseX;
                @MouseX.canceled += instance.OnMouseX;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMouseMovement(InputAction.CallbackContext context);
        void OnPlayerMovement(InputAction.CallbackContext context);
        void OnSwitchCameras(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
        void OnMouseClick(InputAction.CallbackContext context);
        void OnStartPlacingItem(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnUnlockCursor(InputAction.CallbackContext context);
        void OnMouseRightClick(InputAction.CallbackContext context);
        void OnMouseX(InputAction.CallbackContext context);
    }
}
