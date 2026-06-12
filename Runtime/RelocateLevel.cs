using UnityEngine;

namespace Nekuzaky.Xr
{
    public class RelocateLevel : MonoBehaviour
    {
        #region Publics

        [SerializeField] private Transform _levelMove;

        [Header("<color=red> Dbug </color>")] [SerializeField]
        private float _angle;
        #endregion

        #region Unity Api

        public void RelocateFromStartEndPoint(
            Vector3 positionStart,
            Vector3 positionEnd
        )
        {
            float angle = 0;
            Vector3 direction = positionEnd - positionStart;
            Vector3 directionFLatZX= new Vector3(direction.x, 0, direction.z);
            angle = Vector3.SignedAngle(Vector3.forward, directionFLatZX, Vector3.up);
            _levelMove.Rotate(Vector3.up, angle - 90f);
            _angle = angle;
            _levelMove.position += positionStart;
        }
        #endregion
    }
}
