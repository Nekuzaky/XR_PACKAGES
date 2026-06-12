using UnityEngine;

namespace Nekuzaky.Xr
{
    public class XrFollowTarget : MonoBehaviour
    {
        public Transform whatToFollow;
        public Transform whatToMove;

        void Update()
        {
            if (whatToFollow != null && whatToMove != null)
            {
                whatToMove.position = whatToFollow.position;
                whatToMove.rotation = whatToFollow.rotation;
            }
        }
    }
}
