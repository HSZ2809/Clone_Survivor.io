using UnityEngine;
using System.Collections.Generic;

namespace ZUN
{
    public class Manager_Storage : MonoBehaviour
    {
        public int Energy { get; set; }
        public int Gem { get; set; }
        public int Gold { get; set; }

        public readonly List<Equipment> equipments = new();
        public readonly Dictionary<string, Item> items = new();

        public string GetGoldFormatKNotation()
        {
            if (Gold < 10000)
                return Gold.ToString();
            else
                return (Gold * 0.001).ToString() + "k";
        }

        public void AddItem(Item item)
        {
            if (items.ContainsKey(item.Data.Id))
                items[item.Data.Id].Amount += item.Amount;
            else
                items.Add(item.Data.Id, item);
        }
    }
}