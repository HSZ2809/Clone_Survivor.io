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
        public string[] Inventory { get; set; }
        public List<EquipmentInfo> Equipments { get; set; }
        public Dictionary<string, int> Items { get; set; }

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
            Inventory = new string[Enum.GetValues(typeof(EquipmentType)).Length];
            Equipments = new List<EquipmentInfo>();
            Items = new Dictionary<string, int>();
        }

        public UserData()
        {
            Inventory = new string[Enum.GetValues(typeof(EquipmentType)).Length];
            Equipments = new List<EquipmentInfo>();
            Items = new Dictionary<string, int>();
        }
    }

    [Serializable]
    [FirestoreData]
    public class EquipmentInfo
    {
        [FirestoreDocumentId]
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

        public EquipmentInfo(Equipment equip)
        {
            Uuid = equip.Uuid;
            Id = equip.Data.Id;
            Tier = equip.Tier;
            Level = equip.Level;
        }

        public EquipmentInfo()
        {
            Uuid = Guid.NewGuid().ToString();
        }
    }

    [Serializable]
    [FirestoreData]
    public class ItemInfo
    {
        [field: SerializeField]
        [FirestoreDocumentId]
        public string Uid { get; set; }

        [field: SerializeField]
        [FirestoreProperty]
        public int Amount { get; set; }

        public ItemInfo(string id, int amount)
        {
            Uid = id;
            Amount = amount;
        }

        public ItemInfo() { }
    }
}