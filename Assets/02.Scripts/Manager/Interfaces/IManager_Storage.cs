using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public interface IManager_Storage
    {
        int Energy { get; set; }
        int Gem { get; set; }
        int Gold { get; set; }

        List<Equipment> Equipments { get; }
        Dictionary<string, Item> Items { get; }
        
        string GetGoldFormatKNotation();
        void AddItem(Item item);
    }
}