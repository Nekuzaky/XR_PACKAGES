using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

namespace Nekuzaky.Xr
{
    public class XrController
    {
        public readonly XrHand Hand;

        readonly InputActionMap _map;
        readonly InputAction _position;
        readonly InputAction _rotation;
        readonly InputAction _isTracked;
        readonly InputAction _trigger;
        readonly InputAction _triggerPressed;
        readonly InputAction _grip;
        readonly InputAction _gripPressed;
        readonly InputAction _primaryButton;
        readonly InputAction _secondaryButton;
        readonly InputAction _thumbstick;
        readonly InputAction _thumbstickClick;
        readonly InputAction _thumbstickTouch;
        readonly InputAction _menu;

        bool _enabled;

        public event Action<XrController> TriggerDown;
        public event Action<XrController> TriggerUp;
        public event Action<XrController> GripDown;
        public event Action<XrController> GripUp;
        public event Action<XrController> PrimaryButtonDown;
        public event Action<XrController> PrimaryButtonUp;
        public event Action<XrController> SecondaryButtonDown;
        public event Action<XrController> SecondaryButtonUp;
        public event Action<XrController> ThumbstickClickDown;
        public event Action<XrController> ThumbstickClickUp;
        public event Action<XrController> MenuDown;
        public event Action<XrController> MenuUp;

        public XrController(XrHand hand)
        {
            Hand = hand;
            var node = hand == XrHand.Left ? "{LeftHand}" : "{RightHand}";

            _map = new InputActionMap(hand.ToString());
            _position = _map.AddAction("Position", InputActionType.Value, $"<XRController>{node}/devicePosition", expectedControlLayout: "Vector3");
            _rotation = _map.AddAction("Rotation", InputActionType.Value, $"<XRController>{node}/deviceRotation", expectedControlLayout: "Quaternion");
            _isTracked = _map.AddAction("IsTracked", InputActionType.Button, $"<XRController>{node}/isTracked");
            _trigger = _map.AddAction("Trigger", InputActionType.Value, $"<XRController>{node}/trigger", expectedControlLayout: "Axis");
            _triggerPressed = _map.AddAction("TriggerPressed", InputActionType.Button, $"<XRController>{node}/triggerPressed");
            _grip = _map.AddAction("Grip", InputActionType.Value, $"<XRController>{node}/grip", expectedControlLayout: "Axis");
            _gripPressed = _map.AddAction("GripPressed", InputActionType.Button, $"<XRController>{node}/gripPressed");
            _primaryButton = _map.AddAction("PrimaryButton", InputActionType.Button, $"<XRController>{node}/primaryButton");
            _secondaryButton = _map.AddAction("SecondaryButton", InputActionType.Button, $"<XRController>{node}/secondaryButton");
            _thumbstick = _map.AddAction("Thumbstick", InputActionType.Value, $"<XRController>{node}/primary2DAxis", expectedControlLayout: "Vector2");
            _thumbstickClick = _map.AddAction("ThumbstickClick", InputActionType.Button, $"<XRController>{node}/primary2DAxisClick");
            _thumbstickTouch = _map.AddAction("ThumbstickTouch", InputActionType.Button, $"<XRController>{node}/primary2DAxisTouch");
            _menu = hand == XrHand.Left ? _map.AddAction("Menu", InputActionType.Button, $"<XRController>{node}/menuButton") : null;
        }

        public Vector3 Position => _position.ReadValue<Vector3>();
        public Quaternion Rotation => _rotation.ReadValue<Quaternion>();
        public bool IsTracked => _isTracked.IsPressed();
        public float Trigger => _trigger.ReadValue<float>();
        public bool TriggerPressed => _triggerPressed.IsPressed();
        public float Grip => _grip.ReadValue<float>();
        public bool GripPressed => _gripPressed.IsPressed();
        public bool PrimaryButton => _primaryButton.IsPressed();
        public bool SecondaryButton => _secondaryButton.IsPressed();
        public Vector2 Thumbstick => _thumbstick.ReadValue<Vector2>();
        public bool ThumbstickClick => _thumbstickClick.IsPressed();
        public bool ThumbstickTouch => _thumbstickTouch.IsPressed();
        public bool Menu => _menu != null && _menu.IsPressed();

        public void Enable()
        {
            if (_enabled)
                return;
            _enabled = true;

            _triggerPressed.performed += OnTriggerDown;
            _triggerPressed.canceled += OnTriggerUp;
            _gripPressed.performed += OnGripDown;
            _gripPressed.canceled += OnGripUp;
            _primaryButton.performed += OnPrimaryDown;
            _primaryButton.canceled += OnPrimaryUp;
            _secondaryButton.performed += OnSecondaryDown;
            _secondaryButton.canceled += OnSecondaryUp;
            _thumbstickClick.performed += OnThumbstickDown;
            _thumbstickClick.canceled += OnThumbstickUp;

            if (_menu != null)
            {
                _menu.performed += OnMenuDown;
                _menu.canceled += OnMenuUp;
            }

            _map.Enable();
        }

        public void Disable()
        {
            if (!_enabled)
                return;
            _enabled = false;

            _triggerPressed.performed -= OnTriggerDown;
            _triggerPressed.canceled -= OnTriggerUp;
            _gripPressed.performed -= OnGripDown;
            _gripPressed.canceled -= OnGripUp;
            _primaryButton.performed -= OnPrimaryDown;
            _primaryButton.canceled -= OnPrimaryUp;
            _secondaryButton.performed -= OnSecondaryDown;
            _secondaryButton.canceled -= OnSecondaryUp;
            _thumbstickClick.performed -= OnThumbstickDown;
            _thumbstickClick.canceled -= OnThumbstickUp;

            if (_menu != null)
            {
                _menu.performed -= OnMenuDown;
                _menu.canceled -= OnMenuUp;
            }

            _map.Disable();
        }

        public void SendHaptic(float amplitude, float duration)
        {
            var control = _position.activeControl;
            var device = control != null
                ? control.device
                : (_position.controls.Count > 0 ? _position.controls[0].device : null);

            if (device is XRControllerWithRumble rumble)
                rumble.SendImpulse(Mathf.Clamp01(amplitude), duration);
        }

        void OnTriggerDown(InputAction.CallbackContext _) => TriggerDown?.Invoke(this);
        void OnTriggerUp(InputAction.CallbackContext _) => TriggerUp?.Invoke(this);
        void OnGripDown(InputAction.CallbackContext _) => GripDown?.Invoke(this);
        void OnGripUp(InputAction.CallbackContext _) => GripUp?.Invoke(this);
        void OnPrimaryDown(InputAction.CallbackContext _) => PrimaryButtonDown?.Invoke(this);
        void OnPrimaryUp(InputAction.CallbackContext _) => PrimaryButtonUp?.Invoke(this);
        void OnSecondaryDown(InputAction.CallbackContext _) => SecondaryButtonDown?.Invoke(this);
        void OnSecondaryUp(InputAction.CallbackContext _) => SecondaryButtonUp?.Invoke(this);
        void OnThumbstickDown(InputAction.CallbackContext _) => ThumbstickClickDown?.Invoke(this);
        void OnThumbstickUp(InputAction.CallbackContext _) => ThumbstickClickUp?.Invoke(this);
        void OnMenuDown(InputAction.CallbackContext _) => MenuDown?.Invoke(this);
        void OnMenuUp(InputAction.CallbackContext _) => MenuUp?.Invoke(this);
    }
}
