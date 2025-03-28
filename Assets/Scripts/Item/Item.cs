using UnityEngine;

namespace ZUN
{
    public class Item
    {
        public ItemData Data   { get; private set; }
        public int      amount { get; private set; }
        
        public Item(ItemData data) => Data = data;
    }
}
