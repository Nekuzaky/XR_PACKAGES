using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Eloi.TwoPointsLoader
{

    using UnityEngine;
    public class TwoPointsLoad_CreatePrefabFromStartEndPoint : MonoBehaviour
    {

        [SerializeField]
        private UnityEvent<GameObject> m_onCreatedPrefab;

        [SerializeField] private List<TwoPointsLoad_PrefabFromDistance> m_prefabsFromInspector = new List<TwoPointsLoad_PrefabFromDistance>();
        [SerializeField] private TwoPointsLoad_PrefabFromDistanceList m_prefabsFromScriptable;



        public void CreateFromStartEndPoints(Vector3 worldPointStart, Vector3 worldPointEnd) {
            foreach(TwoPointsLoad_PrefabFromDistance p in m_prefabsFromInspector)
            {
                if (p!=null)
                { 
                    CreateFromStartEndPoints(p,worldPointStart,worldPointEnd);
                }
            }
            if (m_prefabsFromScriptable) {
                foreach (TwoPointsLoad_PrefabFromDistance p in m_prefabsFromScriptable.GetListReference())
                {
                    if (p != null)
                    {
                        CreateFromStartEndPoints(p, worldPointStart, worldPointEnd);
                    }
                }
            }

           
        }
        public void CreateFromStartEndPoints(
            TwoPointsLoad_PrefabFromDistance target,
            Vector3 worldPointStart,
            Vector3 worldPointEnd)
        {
            if (target == null) { return; }




            Vector3 start = worldPointStart;
            Vector3 end = worldPointEnd;
            //bool isUpFlat = e.y > 0.05f;
            Vector3 endFlat = end;
            endFlat.y = start.y;
            float distanceStartEndFlat = (endFlat - start).magnitude;
            float delta = Mathf.Abs(target.GetRequiredDistanceInMeter() - distanceStartEndFlat);
            if (delta < target.GetThresholdApproximation())
            {

                GameObject created = GameObject.Instantiate(target.GetPrefabToCreate());
                created.transform.position = Vector3.zero;
                created.transform.rotation = Quaternion.identity;

                Vector3 unityForward = Vector3.forward;
                Vector3 startEndDirection = endFlat - start;


                // We need to compute it
                float rotationToApply = Vector3.SignedAngle(unityForward, startEndDirection, Vector3.up) - 90f;
                created.transform.Rotate(Vector3.up, rotationToApply);

                Vector3 directionStart = worldPointStart;
                created.transform.position = directionStart;

                m_onCreatedPrefab.Invoke(created);
                //return created;
            }
        }
    }
}
