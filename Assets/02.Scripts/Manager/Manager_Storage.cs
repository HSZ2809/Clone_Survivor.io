using UnityEngine;
using System.Collections.Generic;

namespace ZUN
{
    public class Manager_Storage : MonoBehaviour, IManager_Storage
    {
        public int Energy { get; set; }
        public int Gem { get; set; }
        public int Gold { get; set; }

        public List<Equipment> Equipments { get; private set; }
        public Dictionary<string, Item> Items { get; private set; }

        void Awake()
        {
            Equipments = new List<Equipment>();
            Items = new Dictionary<string, Item>();
        }

        public string GetGoldFormatKNotation()
        {
            if (Gold < 10000)
                return Gold.ToString();
            else
                return (Gold * 0.001).ToString() + "k";
        }

        public void AddItem(Item item)
        {
            if (Items.ContainsKey(item.Data.Id))
                Items[item.Data.Id].Amount += item.Amount;
            else
                Items.Add(item.Data.Id, item);
        }
    }
}