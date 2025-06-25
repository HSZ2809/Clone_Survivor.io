using System;
using UnityEngine;

namespace ZUN
{
    public class Manager_Status : MonoBehaviour
    {
        [SerializeField] readonly Equipment[] inventory = new Equipment[Enum.GetValues(typeof(EquipmentType)).Length];
        public Equipment[] Inventory => inventory;

        [SerializeField] float baseHp;
        [SerializeField] float baseAtk;

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
    }
}