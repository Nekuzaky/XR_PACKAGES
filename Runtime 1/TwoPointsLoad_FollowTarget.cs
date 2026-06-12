using UnityEngine;

namespace Eloi.TwoPointsLoader
{
    public class TwoPointsLoad_FollowTarget : MonoBehaviour
    {
        public Transform whatToFollow;
        public Transform whatToMove;

        private void Update()
        {
            if (whatToFollow != null && whatToMove != null)
            {
                whatToMove.position = whatToFollow.position;
                whatToMove.rotation = whatToFollow.rotation;
            }
        }
    }
}
