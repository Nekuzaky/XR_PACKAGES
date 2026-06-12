using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Nekuzaky.Xr
{
    public class XrInputButton : MonoBehaviour
    {
        public InputActionReference m_inputAction;
        public UnityEvent<bool> m_onChanged;
        public UnityEvent m_onDown;
        public UnityEvent m_onUp;
        public bool m_isPressed;

        void OnEnable()
        {
            if (m_inputAction == null || m_inputAction.action == null)
                return;

            m_inputAction.action.Enable();
            m_inputAction.action.started += Context;
            m_inputAction.action.performed += Context;
            m_inputAction.action.canceled += Context;
        }

        void OnDisable()
        {
            if (m_inputAction == null || m_inputAction.action == null)
                return;

            m_inputAction.action.started -= Context;
            m_inputAction.action.performed -= Context;
            m_inputAction.action.canceled -= Context;
            m_inputAction.action.Disable();
        }

        void Context(InputAction.CallbackContext ctx)
        {
            bool isPressed = ctx.ReadValue<float>() > 0.5f;
            if (isPressed != m_isPressed)
            {
                m_isPressed = isPressed;
                m_onChanged.Invoke(isPressed);
                if (m_isPressed)
                    m_onDown.Invoke();
                else
                    m_onUp.Invoke();
            }
        }
    }
}
