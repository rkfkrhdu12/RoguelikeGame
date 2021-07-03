// GENERATED AUTOMATICALLY FROM 'Assets/02.Scripts/InputSystem/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Character"",
            ""id"": ""7f39e577-7971-4088-9d6f-584f0ea180f4"",
            ""actions"": [
                {
                    ""name"": ""X"",
                    ""type"": ""Value"",
                    ""id"": ""eae1ccfb-882f-4cfe-8296-d15f323980f7"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Y"",
                    ""type"": ""Value"",
                    ""id"": ""d1804a77-6c92-4c40-ae2d-9d66a6f0b17d"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Action"",
                    ""type"": ""Button"",
                    ""id"": ""53bdf279-ffc9-4a8b-ab5b-90a4070c369f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Axis"",
                    ""id"": ""0346fa64-15f3-4cb1-b549-631325111fd3"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""X"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""74bb1412-a760-45f2-b156-31dd0b73d623"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""16420521-1ea0-43c2-9048-6d034d3db3de"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""15f53edd-fac7-477e-87ef-705e5d7101f6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Y"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""82d074e4-e609-4eda-8c6c-05ebce60bb26"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c6bd8c83-e175-44af-b2e3-4bf1ca7d804a"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9dc35051-c56f-43ee-9761-ac14607ead00"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Any"",
            ""id"": ""eceaf53c-2ff0-4efa-a710-e16dee29a336"",
            ""actions"": [
                {
                    ""name"": ""ESC"",
                    ""type"": ""Button"",
                    ""id"": ""843d77bb-b262-4ca6-b104-cb6fcad2e36d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""83d43574-8d51-461c-af6f-2abf35e6f3d9"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ESC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Character
        m_Character = asset.FindActionMap("Character", throwIfNotFound: true);
        m_Character_X = m_Character.FindAction("X", throwIfNotFound: true);
        m_Character_Y = m_Character.FindAction("Y", throwIfNotFound: true);
        m_Character_Action = m_Character.FindAction("Action", throwIfNotFound: true);
        // Any
        m_Any = asset.FindActionMap("Any", throwIfNotFound: true);
        m_Any_ESC = m_Any.FindAction("ESC", throwIfNotFound: true);
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

    // Character
    private readonly InputActionMap m_Character;
    private ICharacterActions m_CharacterActionsCallbackInterface;
    private readonly InputAction m_Character_X;
    private readonly InputAction m_Character_Y;
    private readonly InputAction m_Character_Action;
    public struct CharacterActions
    {
        private @PlayerInput m_Wrapper;
        public CharacterActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @X => m_Wrapper.m_Character_X;
        public InputAction @Y => m_Wrapper.m_Character_Y;
        public InputAction @Action => m_Wrapper.m_Character_Action;
        public InputActionMap Get() { return m_Wrapper.m_Character; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterActions instance)
        {
            if (m_Wrapper.m_CharacterActionsCallbackInterface != null)
            {
                @X.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnX;
                @X.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnX;
                @X.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnX;
                @Y.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnY;
                @Y.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnY;
                @Y.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnY;
                @Action.started -= m_Wrapper.m_CharacterActionsCallbackInterface.OnAction;
                @Action.performed -= m_Wrapper.m_CharacterActionsCallbackInterface.OnAction;
                @Action.canceled -= m_Wrapper.m_CharacterActionsCallbackInterface.OnAction;
            }
            m_Wrapper.m_CharacterActionsCallbackInterface = instance;
            if (instance != null)
            {
                @X.started += instance.OnX;
                @X.performed += instance.OnX;
                @X.canceled += instance.OnX;
                @Y.started += instance.OnY;
                @Y.performed += instance.OnY;
                @Y.canceled += instance.OnY;
                @Action.started += instance.OnAction;
                @Action.performed += instance.OnAction;
                @Action.canceled += instance.OnAction;
            }
        }
    }
    public CharacterActions @Character => new CharacterActions(this);

    // Any
    private readonly InputActionMap m_Any;
    private IAnyActions m_AnyActionsCallbackInterface;
    private readonly InputAction m_Any_ESC;
    public struct AnyActions
    {
        private @PlayerInput m_Wrapper;
        public AnyActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @ESC => m_Wrapper.m_Any_ESC;
        public InputActionMap Get() { return m_Wrapper.m_Any; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AnyActions set) { return set.Get(); }
        public void SetCallbacks(IAnyActions instance)
        {
            if (m_Wrapper.m_AnyActionsCallbackInterface != null)
            {
                @ESC.started -= m_Wrapper.m_AnyActionsCallbackInterface.OnESC;
                @ESC.performed -= m_Wrapper.m_AnyActionsCallbackInterface.OnESC;
                @ESC.canceled -= m_Wrapper.m_AnyActionsCallbackInterface.OnESC;
            }
            m_Wrapper.m_AnyActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ESC.started += instance.OnESC;
                @ESC.performed += instance.OnESC;
                @ESC.canceled += instance.OnESC;
            }
        }
    }
    public AnyActions @Any => new AnyActions(this);
    public interface ICharacterActions
    {
        void OnX(InputAction.CallbackContext context);
        void OnY(InputAction.CallbackContext context);
        void OnAction(InputAction.CallbackContext context);
    }
    public interface IAnyActions
    {
        void OnESC(InputAction.CallbackContext context);
    }
}
