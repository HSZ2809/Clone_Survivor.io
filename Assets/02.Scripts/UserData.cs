using Firebase.Firestore;
using System.Collections.Generic;

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
        [FirestoreProperty] public EquipmentDataFair[] Inventory { get; set; }
        [FirestoreProperty] public List<EquipmentDataFair> Equipments { get; set; }
        [FirestoreProperty] public List<ItemDataFair> Items { get; set; }

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
            Inventory = new EquipmentDataFair[6];
            Equipments = new List<EquipmentDataFair>();
            Items = new List<ItemDataFair>();
        }

        public UserData() { }
    }

    [FirestoreData]
    public class EquipmentDataFair
    {
        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public EquipmentTier Tier { get; set; }
        [FirestoreProperty] public int Level { get; set; }
    }

    [FirestoreData]
    public class ItemDataFair
    {
        [FirestoreProperty] public string Id { get; set; }
        [FirestoreProperty] public int Amount { get; set; }
    }
}