using UnityEngine;

namespace ZUN
{
    public class TempItemDispenser : MonoBehaviour
    {
        public Manager_Storage storage;
        public ItemData[] itemDatas;

        private void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Storage>(out storage))
                Debug.LogWarning("Storage not found");
        }

        private void Start()
        {
            for (int i = 0; i < itemDatas.Length; i++)
            {
                for (int k = 0; k < 5; k++)
                {
                    storage.AddItem(itemDatas[i].Create(3));
                }
            }
        }
    }
}