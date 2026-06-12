
using System;
using UnityEngine;

namespace Eloi.TwoPointsLoader
{
    [System.Serializable]
    public class TwoPointsLoad_PrefabFromDistance {
        [SerializeField] private GameObject m_prefabToCreated;
        [SerializeField] private float m_requiredDistanceInMeter = 0.21f;
        [SerializeField] private float m_threshold = 0.02f;

        public GameObject GetPrefabToCreate()
        {
            return m_prefabToCreated;
        }

        public float GetRequiredDistanceInMeter()
        {
            return m_requiredDistanceInMeter;
        }

        public float GetThresholdApproximation()
        {
            return m_threshold;
        }
    }
}
