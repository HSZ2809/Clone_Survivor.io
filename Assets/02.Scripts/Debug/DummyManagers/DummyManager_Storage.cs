using System.Collections.Generic;

namespace ZUN
{
    public class DummyManager_Storage : IManager_Storage
    {
        public int Energy { get; set; } = 30;
        public int Gem { get; set; } = 0;
        public int Gold { get; set; } = 0;
        public List<Equipment> Equipments { get; } = new();
        public Dictionary<string, Item> Items { get; } = new();

        public string GetGoldFormatKNotation() => Gold.ToString();
        public void AddItem(Item item) { }
    }
}
