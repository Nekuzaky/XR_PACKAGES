using System.Collections.Generic;
namespace Eloi.TwoPointsLoader
{
    using System;
    using UnityEngine;

    // Use the CreateAssetMenu attribute to allow creating instances of this ScriptableObject from the Unity Editor.
    [CreateAssetMenu(fileName = "PrefabFromDistanceList", menuName = "ScriptableObjects/Prefab From Distance List", order = 1)]
    public class TwoPointsLoad_PrefabFromDistanceList : ScriptableObject
    {
        [SerializeField]
        private List<TwoPointsLoad_PrefabFromDistance> m_prefabs = new List<TwoPointsLoad_PrefabFromDistance>();
        public List<TwoPointsLoad_PrefabFromDistance> GetListReference()
        {
            return m_prefabs;
        }
    }
}
