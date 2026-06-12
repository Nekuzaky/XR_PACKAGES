using UnityEngine;
using UnityEngine.Events;

namespace Nekuzaky.Xr
{
    public class XrTwoPointsEmitter : MonoBehaviour
    {
        [SerializeField] UnityEvent<Vector3, Vector3> m_onStartEndPointEmitted;
        [SerializeField] Transform m_startPoint;
        [SerializeField] Transform m_endPoint;
        [SerializeField] Transform m_cursorToSetPoints;

        [ContextMenu("Emit Current Points")]
        public void EmitCurrentPoint()
        {
            m_onStartEndPointEmitted?.Invoke(m_startPoint.position, m_endPoint.position);
        }

        public void SetStartPointToCursor()
        {
            m_startPoint.position = m_cursorToSetPoints.position;
        }

        public void SetEndPointToCursor()
        {
            m_endPoint.position = m_cursorToSetPoints.position;
        }
    }
}
