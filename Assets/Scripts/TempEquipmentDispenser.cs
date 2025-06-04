using UnityEngine;

namespace ZUN
{
    public class TempEquipmentDispenser : MonoBehaviour
    {
        public Manager_Status status;
        public Manager_Storage storage;
        public GameStart gameStart;

        public EquipmentData[] EquipmentDatas;

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
                status.Inventory[i] = EquipmentDatas[i].Create(EquipmentTier.Common);
            }

            for (int i = 0; i < 20; i++)
            {
                storage.equipments.Add(EquipmentDatas[(int)EquipmentType.Weapon].Create(EquipmentTier.Common));
            }

            for (int i = 0; i < 20; i++)
            {
                storage.equipments.Add(EquipmentDatas[(int)EquipmentType.Armor].Create(EquipmentTier.Common));
            }

            gameStart.tempEquipmentDispenserLoding = true;
        }
    }
}