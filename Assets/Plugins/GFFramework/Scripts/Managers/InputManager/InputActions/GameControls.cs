// GENERATED AUTOMATICALLY FROM 'Assets/Plugins/GFFramework/Scripts/Managers/InputManager/InputActions/GameControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace GFF.InputsMan.InputActions
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
            ""name"": ""UIScreen"",
            ""id"": ""c4f7712f-2659-48fd-b8bd-fed214b73c98"",
            ""actions"": [
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
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
    ""controlSchemes"": [
        {
            ""name"": ""New control scheme"",
            ""bindingGroup"": ""New control scheme"",
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
            // UIScreen
            m_UIScreen = asset.FindActionMap("UIScreen", throwIfNotFound: true);
            m_UIScreen_Back = m_UIScreen.FindAction("Back", throwIfNotFound: true);
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

        // UIScreen
        private readonly InputActionMap m_UIScreen;
        private IUIScreenActions m_UIScreenActionsCallbackInterface;
        private readonly InputAction m_UIScreen_Back;
        public struct UIScreenActions
        {
            private @GameControls m_Wrapper;
            public UIScreenActions(@GameControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Back => m_Wrapper.m_UIScreen_Back;
            public InputActionMap Get() { return m_Wrapper.m_UIScreen; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIScreenActions set) { return set.Get(); }
            public void SetCallbacks(IUIScreenActions instance)
            {
                if (m_Wrapper.m_UIScreenActionsCallbackInterface != null)
                {
                    @Back.started -= m_Wrapper.m_UIScreenActionsCallbackInterface.OnBack;
                    @Back.performed -= m_Wrapper.m_UIScreenActionsCallbackInterface.OnBack;
                    @Back.canceled -= m_Wrapper.m_UIScreenActionsCallbackInterface.OnBack;
                }
                m_Wrapper.m_UIScreenActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Back.started += instance.OnBack;
                    @Back.performed += instance.OnBack;
                    @Back.canceled += instance.OnBack;
                }
            }
        }
        public UIScreenActions @UIScreen => new UIScreenActions(this);
        private int m_NewcontrolschemeSchemeIndex = -1;
        public InputControlScheme NewcontrolschemeScheme
        {
            get
            {
                if (m_NewcontrolschemeSchemeIndex == -1) m_NewcontrolschemeSchemeIndex = asset.FindControlSchemeIndex("New control scheme");
                return asset.controlSchemes[m_NewcontrolschemeSchemeIndex];
            }
        }
        public interface IUIScreenActions
        {
            void OnBack(InputAction.CallbackContext context);
        }
    }
}
