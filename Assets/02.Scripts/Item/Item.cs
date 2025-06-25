using UnityEngine;

namespace ZUN
{
    public class Item
    {
        public ItemData Data { get; private set; }
        int _amount;
        public int Amount
        {
            get { return _amount; }
            set
            {
                if (_amount + value < 0)
                    Debug.LogWarning("Item : Amount Set Error");
                else
                    _amount = value;
            }
        }

        public Item(ItemData data, int amount)
        {
            Data = data;
            Amount = amount;
        }
    }
}