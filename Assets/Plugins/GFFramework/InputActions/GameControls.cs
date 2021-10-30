// GENERATED AUTOMATICALLY FROM 'Assets/Plugins/GFFramework/InputActions/GameControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace GFFramework.Input
{
    public class @GameControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @GameControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""IdleState"",
            ""id"": ""a5aa67cb-3209-4fd9-837c-19f64495c2f2"",
            ""actions"": [
                {
                    ""name"": ""MainAction"",
                    ""type"": ""PassThrough"",
                    ""id"": ""33cb3cc7-572f-4849-844a-eb5a41136b99"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""b6886db7-951d-4bdc-bbe1-da3a900518ed"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c96b6d3c-094f-4cfc-aaa5-931a67c07fc2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MainAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""ARROWS"",
                    ""id"": ""5a7bd6fd-5b2d-4d5d-b6e9-0d53b8bb729b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d7070a16-b596-4c91-beee-3c1c89ece10b"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b7febcf9-051d-4a29-9d39-fd3c88b79e23"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1b8dd675-3a8e-431e-bc30-31ca30e56aec"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""97991683-ee57-41b5-ab0a-e98b5bb77927"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""50e79de7-7f4e-4b8d-8638-2a6f7340bfd8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""70c833aa-98f8-417f-a3e4-0719a6036027"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3208333c-07dd-4e3f-9e95-4befd8c17eef"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7f39b5e6-0ab7-44b9-9921-9c0cf48081b4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f4ab5388-db9b-4cd1-94cc-ac7695ea5857"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""UIState"",
            ""id"": ""c4f7712f-2659-48fd-b8bd-fed214b73c98"",
            ""actions"": [
                {
                    ""name"": ""Back"",
                    ""type"": ""PassThrough"",
                    ""id"": ""79176f1e-30dd-4eb0-a066-dc5b44eaa458"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c895be3f-b1b9-4345-aad1-468b14d105e6"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // IdleState
            m_IdleState = asset.FindActionMap("IdleState", throwIfNotFound: true);
            m_IdleState_MainAction = m_IdleState.FindAction("MainAction", throwIfNotFound: true);
            m_IdleState_Move = m_IdleState.FindAction("Move", throwIfNotFound: true);
            // UIState
            m_UIState = asset.FindActionMap("UIState", throwIfNotFound: true);
            m_UIState_Back = m_UIState.FindAction("Back", throwIfNotFound: true);
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

        // IdleState
        private readonly InputActionMap m_IdleState;
        private IIdleStateActions m_IdleStateActionsCallbackInterface;
        private readonly InputAction m_IdleState_MainAction;
        private readonly InputAction m_IdleState_Move;
        public struct IdleStateActions
        {
            private @GameControls m_Wrapper;
            public IdleStateActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @MainAction => m_Wrapper.m_IdleState_MainAction;
            public InputAction @Move => m_Wrapper.m_IdleState_Move;
            public InputActionMap Get() { return m_Wrapper.m_IdleState; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(IdleStateActions set) { return set.Get(); }
            public void SetCallbacks(IIdleStateActions instance)
            {
                if (m_Wrapper.m_IdleStateActionsCallbackInterface != null)
                {
                    @MainAction.started -= m_Wrapper.m_IdleStateActionsCallbackInterface.OnMainAction;
                    @MainAction.performed -= m_Wrapper.m_IdleStateActionsCallbackInterface.OnMainAction;
                    @MainAction.canceled -= m_Wrapper.m_IdleStateActionsCallbackInterface.OnMainAction;
                    @Move.started -= m_Wrapper.m_IdleStateActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_IdleStateActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_IdleStateActionsCallbackInterface.OnMove;
                }
                m_Wrapper.m_IdleStateActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @MainAction.started += instance.OnMainAction;
                    @MainAction.performed += instance.OnMainAction;
                    @MainAction.canceled += instance.OnMainAction;
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                }
            }
        }
        public IdleStateActions @IdleState => new IdleStateActions(this);

        // UIState
        private readonly InputActionMap m_UIState;
        private IUIStateActions m_UIStateActionsCallbackInterface;
        private readonly InputAction m_UIState_Back;
        public struct UIStateActions
        {
            private @GameControls m_Wrapper;
            public UIStateActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Back => m_Wrapper.m_UIState_Back;
            public InputActionMap Get() { return m_Wrapper.m_UIState; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIStateActions set) { return set.Get(); }
            public void SetCallbacks(IUIStateActions instance)
            {
                if (m_Wrapper.m_UIStateActionsCallbackInterface != null)
                {
                    @Back.started -= m_Wrapper.m_UIStateActionsCallbackInterface.OnBack;
                    @Back.performed -= m_Wrapper.m_UIStateActionsCallbackInterface.OnBack;
                    @Back.canceled -= m_Wrapper.m_UIStateActionsCallbackInterface.OnBack;
                }
                m_Wrapper.m_UIStateActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Back.started += instance.OnBack;
                    @Back.performed += instance.OnBack;
                    @Back.canceled += instance.OnBack;
                }
            }
        }
        public UIStateActions @UIState => new UIStateActions(this);
        public interface IIdleStateActions
        {
            void OnMainAction(InputAction.CallbackContext context);
            void OnMove(InputAction.CallbackContext context);
        }
        public interface IUIStateActions
        {
            void OnBack(InputAction.CallbackContext context);
        }
    }
}
