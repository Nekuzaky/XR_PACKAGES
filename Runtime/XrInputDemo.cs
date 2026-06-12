using UnityEngine;

namespace Nekuzaky.Xr
{
    public class XrInputDemo : MonoBehaviour
    {
        [SerializeField] XrInputManager input;
        [SerializeField] float hapticAmplitude = 0.5f;
        [SerializeField] float hapticDuration = 0.1f;

        bool _subscribed;

        void Start()
        {
            if (input == null)
            {
                input = FindObjectOfType<XrInputManager>();
                if (input == null)
                    return;
            }

            input.Left.PrimaryButtonDown += OnLeftPrimary;
            input.Left.SecondaryButtonDown += OnLeftSecondary;
            input.Left.MenuDown += OnMenu;
            input.Right.PrimaryButtonDown += OnRightPrimary;
            input.Right.SecondaryButtonDown += OnRightSecondary;
            input.Right.TriggerDown += OnRightTrigger;
            input.Right.GripDown += OnRightGrip;
            _subscribed = true;
        }

        void OnDestroy()
        {
            if (!_subscribed || input == null)
                return;

            input.Left.PrimaryButtonDown -= OnLeftPrimary;
            input.Left.SecondaryButtonDown -= OnLeftSecondary;
            input.Left.MenuDown -= OnMenu;
            input.Right.PrimaryButtonDown -= OnRightPrimary;
            input.Right.SecondaryButtonDown -= OnRightSecondary;
            input.Right.TriggerDown -= OnRightTrigger;
            input.Right.GripDown -= OnRightGrip;
        }

        void Update()
        {
            if (input == null)
                return;

            var stick = input.Left.Thumbstick;
            if (stick.sqrMagnitude > 0.04f)
                Debug.Log($"Left stick {stick} | Right trigger {input.Right.Trigger:0.00}");
        }

        void OnLeftPrimary(XrController c) => Debug.Log("Left X");
        void OnLeftSecondary(XrController c) => Debug.Log("Left Y");
        void OnMenu(XrController c) => Debug.Log("Menu");
        void OnRightPrimary(XrController c) => Debug.Log("Right A");
        void OnRightSecondary(XrController c) => Debug.Log("Right B");
        void OnRightGrip(XrController c) => Debug.Log("Right grip");

        void OnRightTrigger(XrController c)
        {
            Debug.Log("Right trigger");
            c.SendHaptic(hapticAmplitude, hapticDuration);
        }
    }
}
