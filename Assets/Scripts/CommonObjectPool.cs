using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class CommonObjectPool : MonoBehaviour
    {
        [System.Serializable]
        public class PrefabData
        {
            public string name;
            public GameObject prefab;
        }

        [SerializeField] PrefabData[] prefabDatas;
        [HideInInspector] public readonly Dictionary<string, Queue<GameObject>> pool = new();

        private void Start()
        {
            foreach (var data in prefabDatas)
            {
                pool.Add(data.name, new Queue<GameObject>());
            }
        }

        // public GameObject Get(string objectName, )
    }
}