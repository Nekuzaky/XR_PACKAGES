using UnityEngine;
using UnityEngine.Events;

namespace Nekuzaky.Xr
{
    public class XrTwoPointsLoader : MonoBehaviour
    {
        [SerializeField] UnityEvent<GameObject> m_onCreatedPrefab;
        [SerializeField] GameObject m_prefabToCreate;
        [SerializeField] float m_requiredDistanceInMeter = 0.21f;
        [SerializeField] float m_threshold = 0.2f;

        public void CreateFromStartEndPoints(Vector3 worldPointStart, Vector3 worldPointEnd)
        {
            Vector3 start = worldPointStart;
            Vector3 endFlat = worldPointEnd;
            endFlat.y = start.y;

            float distanceStartEndFlat = (endFlat - start).magnitude;
            float delta = Mathf.Abs(m_requiredDistanceInMeter - distanceStartEndFlat);
            if (delta >= m_threshold)
                return;

            GameObject created = Instantiate(m_prefabToCreate);
            created.transform.position = Vector3.zero;
            created.transform.rotation = Quaternion.identity;

            Vector3 startEndDirection = endFlat - start;
            float rotationToApply = Vector3.SignedAngle(Vector3.forward, startEndDirection, Vector3.up) - 90f;
            created.transform.Rotate(Vector3.up, rotationToApply);
            created.transform.position = worldPointStart;

            m_onCreatedPrefab.Invoke(created);
        }
    }
}
