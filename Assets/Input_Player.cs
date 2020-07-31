// GENERATED AUTOMATICALLY FROM 'Assets/Input_Player.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Input_Player : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input_Player()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input_Player"",
    ""maps"": [
        {
            ""name"": ""Cube"",
            ""id"": ""e80f5ef8-e396-4f3d-890f-a5cc8e58d85c"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""beac471b-9e42-4dde-87fe-9b1a8ad94b2b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""92f97ccc-8bc0-4599-9520-f88c62628116"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""0c59c46e-d67d-4d18-8a05-150efd9bb40e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Value"",
                    ""id"": ""d234c228-814e-4457-a7e7-e006dc42f7d6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""f28c0785-7733-4fad-ad33-a81e83ec5713"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RJoystick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1574a8e2-4e2d-436c-92dd-b958563d2e34"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5ddb2024-0e23-423e-8d9f-18924b6a0b5c"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3911c9d-17ad-4a4c-aa86-8e0304faf831"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dbb1ade0-85de-4d1e-a543-47bdd505961e"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a669c441-7d9d-497c-9977-4da37b4d5825"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b85f3ecc-6955-4f0a-904b-c5c36d594426"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""DPad"",
                    ""id"": ""9048f03c-de89-49f5-ab61-2d652ad6946c"",
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
                    ""id"": ""5671da24-2435-4311-a0dc-1b687dbbf851"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""33b862d7-dd17-42b9-8234-6d820d22a9fc"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cb9aca22-d519-4dbb-a11a-c69d0e29d56c"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b23f9632-6b25-43f4-9be2-087491af313a"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""b7d1a75d-c3be-4814-b8f1-cd379a62da21"",
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
                    ""id"": ""d73736cb-f7c6-49c0-8219-9bdd807399be"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""363dd4f2-0a9c-4ee8-a62f-0e2bea2fd123"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a7e2d00d-b07a-4393-b50b-83b75f3bac26"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2699f709-751f-43cc-a6d3-107a131dbbdb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9061d21f-c03a-4fe4-aa96-ff244d8fb94d"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RJoystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Cube
        m_Cube = asset.FindActionMap("Cube", throwIfNotFound: true);
        m_Cube_Jump = m_Cube.FindAction("Jump", throwIfNotFound: true);
        m_Cube_Dash = m_Cube.FindAction("Dash", throwIfNotFound: true);
        m_Cube_Shoot = m_Cube.FindAction("Shoot", throwIfNotFound: true);
        m_Cube_Block = m_Cube.FindAction("Block", throwIfNotFound: true);
        m_Cube_Movement = m_Cube.FindAction("Movement", throwIfNotFound: true);
        m_Cube_RJoystick = m_Cube.FindAction("RJoystick", throwIfNotFound: true);
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

    // Cube
    private readonly InputActionMap m_Cube;
    private ICubeActions m_CubeActionsCallbackInterface;
    private readonly InputAction m_Cube_Jump;
    private readonly InputAction m_Cube_Dash;
    private readonly InputAction m_Cube_Shoot;
    private readonly InputAction m_Cube_Block;
    private readonly InputAction m_Cube_Movement;
    private readonly InputAction m_Cube_RJoystick;
    public struct CubeActions
    {
        private @Input_Player m_Wrapper;
        public CubeActions(@Input_Player wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Cube_Jump;
        public InputAction @Dash => m_Wrapper.m_Cube_Dash;
        public InputAction @Shoot => m_Wrapper.m_Cube_Shoot;
        public InputAction @Block => m_Wrapper.m_Cube_Block;
        public InputAction @Movement => m_Wrapper.m_Cube_Movement;
        public InputAction @RJoystick => m_Wrapper.m_Cube_RJoystick;
        public InputActionMap Get() { return m_Wrapper.m_Cube; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CubeActions set) { return set.Get(); }
        public void SetCallbacks(ICubeActions instance)
        {
            if (m_Wrapper.m_CubeActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_CubeActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_CubeActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_CubeActionsCallbackInterface.OnJump;
                @Dash.started -= m_Wrapper.m_CubeActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_CubeActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_CubeActionsCallbackInterface.OnDash;
                @Shoot.started -= m_Wrapper.m_CubeActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_CubeActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_CubeActionsCallbackInterface.OnShoot;
                @Block.started -= m_Wrapper.m_CubeActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_CubeActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_CubeActionsCallbackInterface.OnBlock;
                @Movement.started -= m_Wrapper.m_CubeActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CubeActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CubeActionsCallbackInterface.OnMovement;
                @RJoystick.started -= m_Wrapper.m_CubeActionsCallbackInterface.OnRJoystick;
                @RJoystick.performed -= m_Wrapper.m_CubeActionsCallbackInterface.OnRJoystick;
                @RJoystick.canceled -= m_Wrapper.m_CubeActionsCallbackInterface.OnRJoystick;
            }
            m_Wrapper.m_CubeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @RJoystick.started += instance.OnRJoystick;
                @RJoystick.performed += instance.OnRJoystick;
                @RJoystick.canceled += instance.OnRJoystick;
            }
        }
    }
    public CubeActions @Cube => new CubeActions(this);
    public interface ICubeActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnRJoystick(InputAction.CallbackContext context);
    }
}
