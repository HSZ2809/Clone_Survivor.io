using UnityEngine;

namespace ZUN
{
    public class Manager_Status : MonoBehaviour
    {
        // Inventory 를 따로 안하고 equipment[] 6개로 해서 관리할 것
        public struct Inventory
        {
            public Weapon weapon;
            public Armor armor;
            public Necklace necklace;
            public Belt belt;
            public Gloves gloves;
            public Shoes shoes;
        }
        public Inventory inventory;

        [SerializeField] float baseHp;
        [SerializeField] float baseAtk;

        //public float FinalHp { get; private set; }
        public float FinalHp
        {
            get
            {
                float totalHp = 0.0f;
                float totalCoefficient = 1.0f;

                totalHp += baseHp;

                if (inventory.armor != null)
                {
                    totalHp += inventory.armor.Hp;
                    totalCoefficient += inventory.armor.Coefficient;
                }
                if (inventory.belt != null)
                {
                    totalHp += inventory.belt.Hp;
                    totalCoefficient += inventory.belt.Coefficient;
                }
                if (inventory.shoes != null)
                {
                    totalHp += inventory.shoes.Hp;
                    totalCoefficient += inventory.shoes.Coefficient;
                }

                return totalHp * totalCoefficient;
            }
        }

        //public float FinalAtk { get; private set; }
        public float FinalAtk
        {
            get
            {
                float totalAtk = 0.0f;
                float totalCoefficient = 1.0f;

                totalAtk += baseAtk;

                if (inventory.weapon != null)
                {
                    totalAtk += inventory.weapon.Atk;
                    totalCoefficient += inventory.weapon.Coefficient;
                }
                if (inventory.necklace != null)
                {
                    totalAtk += inventory.necklace.Atk;
                    totalCoefficient += inventory.necklace.Coefficient;
                }
                if (inventory.gloves != null)
                {
                    totalAtk += inventory.gloves.Atk;
                    totalCoefficient += inventory.gloves.Coefficient;
                }

                return totalAtk * totalCoefficient;
            }
        }

        //void UpdateHp()
        //{
        //    float totalHp = 0.0f;
        //    float totalCoefficient = 1.0f;

        //    totalHp += baseHp;

        //    if (inventory.armor != null)
        //    {
        //        totalHp += inventory.armor.Hp;
        //        totalCoefficient += inventory.armor.Coefficient;
        //    }
        //    if (inventory.belt != null)
        //    {
        //        totalHp += inventory.belt.Hp;
        //        totalCoefficient += inventory.belt.Coefficient;
        //    }
        //    if (inventory.shoes != null)
        //    {
        //        totalHp += inventory.shoes.Hp;
        //        totalCoefficient += inventory.shoes.Coefficient;
        //    }
        //    Debug.Log("HP : " + totalHp + ", Coefficient : " + totalCoefficient);
        //    FinalHp = totalHp * totalCoefficient;
        //}

        //void UpdateAtk()
        //{
        //    float totalAtk = 0.0f;
        //    float totalCoefficient = 1.0f;

        //    totalAtk += baseAtk;

        //    if (inventory.weapon != null)
        //    {
        //        totalAtk += inventory.weapon.Atk;
        //        totalCoefficient += inventory.weapon.Coefficient;
        //    }
        //    if (inventory.necklace != null)
        //    {
        //        totalAtk += inventory.necklace.Atk;
        //        totalCoefficient += inventory.necklace.Coefficient;
        //    }
        //    if (inventory.gloves != null)
        //    {
        //        totalAtk += inventory.gloves.Atk;
        //        totalCoefficient += inventory.gloves.Coefficient;
        //    }
        //    Debug.Log("HP : " + totalAtk + ", Coefficient : " + totalCoefficient);
        //    FinalAtk = totalAtk * totalCoefficient;
        //}
    }
}