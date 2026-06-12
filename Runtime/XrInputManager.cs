using UnityEngine;

namespace Nekuzaky.Xr
{
    public class XrInputManager : MonoBehaviour
    {
        public XrController Left { get; private set; }
        public XrController Right { get; private set; }

        public XrController Get(XrHand hand) => hand == XrHand.Left ? Left : Right;

        void Awake()
        {
            Left = new XrController(XrHand.Left);
            Right = new XrController(XrHand.Right);
        }

        void OnEnable()
        {
            Left?.Enable();
            Right?.Enable();
        }

        void OnDisable()
        {
            Left?.Disable();
            Right?.Disable();
        }
    }
}
