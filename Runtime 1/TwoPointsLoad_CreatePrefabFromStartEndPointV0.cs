using UnityEngine;
using UnityEngine.Events;

namespace Eloi.TwoPointsLoader
{
    public class TwoPointsLoad_CreatePrefabFromStartEndPointV0 : MonoBehaviour
    {

        [SerializeField]
        private UnityEvent<GameObject> m_onCreatedPrefab;

        [SerializeField] private GameObject m_prefabToCreated;
        [SerializeField] private float m_requiredDistanceInMeter = 0.21f;
        [SerializeField] private float m_threshold = 0.2f;



        public void CreateFromStartEndPoints(Vector3 worldPointStart, Vector3 worldPointEnd) {
            // Code is bad an not reusable. It is just for a first draft example of the library.

           


            Vector3 start = worldPointStart;
            Vector3 end = worldPointEnd;
            //bool isUpFlat = e.y > 0.05f;
            Vector3 endFlat = end;
            endFlat.y = start.y;
            float distanceStartEndFlat = (endFlat - start).magnitude;
            float delta = Mathf.Abs(m_requiredDistanceInMeter - distanceStartEndFlat);
            if (delta < m_threshold) {

                GameObject created = GameObject.Instantiate(m_prefabToCreated);
                created.transform.position = Vector3.zero;
                created.transform.rotation = Quaternion.identity;

                Vector3 unityForward = Vector3.forward;
                Vector3 startEndDirection = endFlat-start;


                // We need to compute it
                float rotationToApply = Vector3.SignedAngle(unityForward,startEndDirection, Vector3.up)-90f;
                created.transform.Rotate(Vector3.up, rotationToApply);

                Vector3 directionStart = worldPointStart;
                created.transform.position = directionStart;

                m_onCreatedPrefab.Invoke(created);
                //return created;
            }
        }
    }
}
