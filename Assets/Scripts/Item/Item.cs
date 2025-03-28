using UnityEngine;

namespace ZUN
{
    public class Item
    {
        public ItemData Data   { get; private set; }
        public int      Amount { get; set; }
        
        public Item(ItemData data, int amount)
        {
            Data = data;
            Amount = amount;
        }
    }
}
