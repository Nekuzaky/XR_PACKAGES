using UnityEngine;
using UnityEngine.Events;

namespace Eloi.TwoPointsLoader
{
    public class TwoPointsLoad_EmitStartEndPoints : MonoBehaviour
    {

        [SerializeField] private UnityEvent<Vector3, Vector3> m_onStartEndPointEmitted;

        [SerializeField] private Transform m_startPoint;
        [SerializeField] private Transform m_endPoint;
        [SerializeField] private Transform m_cursorToSetPoints;

        [ContextMenu("Emit Current Points")]
        public void EmitCurrentPoint() {
            m_onStartEndPointEmitted?.Invoke(m_startPoint.position, m_endPoint.position);
        }

        public void SetStartPointToCursor() { m_startPoint.transform.position = m_cursorToSetPoints.position; }
        public void SetEndPointToCursor() { m_endPoint.transform.position = m_cursorToSetPoints.position; }
    }
}
