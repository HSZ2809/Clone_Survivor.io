using System;
using UnityEngine;

namespace ZUN
{
    public class Manager_Status : MonoBehaviour
    {
        UserDataManager userDataManager;
        GameDataProvider gameDataProvider;

        [SerializeField] readonly Equipment[] inventory = new Equipment[Enum.GetValues(typeof(EquipmentType)).Length];
        public Equipment[] Inventory => inventory;

        [SerializeField] float baseHp;
        public float FinalHp
        {
            get
            {
                float totalHp = 0.0f;
                float totalCoefficient = 1.0f;

                totalHp += baseHp;
                
                for(int i = (int)EquipmentType.Armor; i < inventory.Length; i += 2)
                {
                    if (inventory[i] == null) continue;

                    totalHp += inventory[i].Stat;
                    totalCoefficient += inventory[i].Coefficient;
                }

                return totalHp * totalCoefficient;
            }
        }
        
        [SerializeField] float baseAtk;
        public float FinalAtk
        {
            get
            {
                float totalAtk = 0.0f;
                float totalCoefficient = 1.0f;

                totalAtk += baseAtk;

                for (int i = (int)EquipmentType.Weapon; i < inventory.Length; i += 2)
                {
                    if (inventory[i] == null) continue;

                    totalAtk += inventory[i].Stat;
                    totalCoefficient += inventory[i].Coefficient;
                }

                return totalAtk * totalCoefficient;
            }
        }

        private void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<UserDataManager>(out userDataManager))
                Debug.LogWarning("UserDataManager not found");

            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<GameDataProvider>(out gameDataProvider))
                Debug.LogWarning("GameDataProvider not found");
        }

        private void Start()
        {
            //userDataManager.OnChanged += InitInventory;
        }
    }
}