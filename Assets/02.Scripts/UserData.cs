using Firebase.Firestore;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    [FirestoreData]
    public class UserData
    {
        [FirestoreProperty] public string Email { get; set; }
        [FirestoreProperty] public string Name { get; set; }
        [FirestoreProperty] public int Level { get; set; }
        [FirestoreProperty] public int Exp { get; set; }
        [FirestoreProperty] public int Energy { get; set; }
        [FirestoreProperty] public int Gem { get; set; }
        [FirestoreProperty] public int Gold { get; set; }
        [FirestoreProperty] public bool IsTutorialCompleted { get; set; }
        [FirestoreProperty] public EquipmentInfomation[] Inventory { get; set; }
        [FirestoreProperty] public List<EquipmentInfomation> Equipments { get; set; }
        [FirestoreProperty] public List<ItemInfomation> Items { get; set; }

        public UserData(string email, string username, int userLevel)
        {
            Email = email;
            Name = username;
            Level = userLevel;
            Exp = 0;
            Energy = 30;
            Gem = 0;
            Gold = 0;
            IsTutorialCompleted = false;
            Inventory = new EquipmentInfomation[6];
            Equipments = new List<EquipmentInfomation>();
            Items = new List<ItemInfomation>();
        }

        public UserData() { }
    }

    //[Serializable]
    //[FirestoreData]
    //public class EquipmentInfomation
    //{
    //    [FirestoreProperty] public string Uuid { get; set; }
    //    [FirestoreProperty] public string Id { get; set; }
    //    [FirestoreProperty] public EquipmentTier Tier { get; set; }
    //    [FirestoreProperty] public int Level { get; set; }
    //}

    [Serializable]
    [FirestoreData]
    public class EquipmentInfomation
    {
        [FirestoreProperty]
        public string Uuid { get; set; }

        [field: SerializeField]
        [FirestoreProperty]
        public string Id { get; set; }

        [field: SerializeField]
        [FirestoreProperty]
        public EquipmentTier Tier { get; set; }

        [field: SerializeField]
        [FirestoreProperty]
        public int Level { get; set; }
    }

    [FirestoreData]
    public class ItemInfomation
    {
        [FirestoreProperty] public string Uuid { get; set; }
        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public int Amount { get; set; }
    }
}