using UnityEngine;

namespace ZUN
{
    public abstract class EquipmentData : ScriptableObject
    {
        [SerializeField] int id;
        [SerializeField] string itemName;
        [TextArea(2, 3)]
        [SerializeField] string comment;
        [SerializeField] Sprite[] iconSprite;
        [SerializeField] int[] maxLevel;
        [SerializeField] EquipmentType type;

        public int Id => id;
        public string ItemName => itemName;
        public string Comment => comment;
        public Sprite[] IconSprite => iconSprite;
        public int[] MaxLevel   => maxLevel;
        public EquipmentType Type => type;

        public enum EquipmentType
        {
            Weapom,
            Cloth,
            Necklace,
            Belt,
            Gloves,
            Shoes
        }

        public abstract Equipment Create(EquipmentTier tier);
    }
}
