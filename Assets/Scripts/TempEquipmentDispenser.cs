using UnityEngine;

namespace ZUN
{
    public class TempEquipmentDispenser : MonoBehaviour
    {
        public Manager_Status status;
        public Manager_Storage storage;
        public GameStart gameStart;

        public EquipmentData[] invenDatas;
        public EquipmentData[] storageDatas;

        private void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Status>(out status))
                Debug.LogWarning("Inventory not found");

            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Storage>(out storage))
                Debug.LogWarning("Storage not found");
        }

        public void Start()
        {
            for(int i = 0; i < status.Inventory.Length; i++)
            {
                status.Inventory[i] = invenDatas[i].Create(EquipmentTier.Common);
            }

            foreach (EquipmentData equipData in storageDatas)
            {
                for (int i = 0; i < 5; i++)
                    storage.equipments.Add(equipData.Create(EquipmentTier.Common));
            }

            gameStart.tempEquipmentDispenserLoding = true;
        }
    }
}