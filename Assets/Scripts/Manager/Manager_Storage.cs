using UnityEngine;
using System.Collections.Generic;

namespace ZUN
{
    public class Manager_Storage : MonoBehaviour
    {
        public int Energy { get; private set; }
        public int Gem { get; private set; }
        public int Gold { get; private set; }

        public readonly List<Equipment> equipments = new();
        public readonly Dictionary<string, Item> items = new();

        private void Start()
        {
            // 기능 확인용 임시 코드
            Energy = 30;
            Gem = 1000;
            Gold = 100000;
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
            if (items.ContainsKey(item.Data.Id))
                items[item.Data.Id].Amount += item.Amount;
            else
                items.Add(item.Data.Id, item);
        }
    }
}