using UnityEngine;

namespace ZUN
{
    public enum EquipmentType
    {
        Weapom,
        Armor,
        Necklace,
        Belt,
        Gloves,
        Shoes
    }

    public abstract class EquipmentData : ScriptableObject
    {
        [SerializeField] int id;
        [SerializeField] string equipmentName;
        [TextArea(2, 3)]
        [SerializeField] string comment;
        [SerializeField] Sprite[] iconSprite;
        [SerializeField] int[] maxLevel;
        [SerializeField] EquipmentType type;

        public int Id => id;
        public string EquipmentName => equipmentName;
        public string Comment => comment;
        public Sprite[] IconSprite => iconSprite;
        public int[] MaxLevel   => maxLevel;
        public EquipmentType Type 
        { 
            get {  return type; } 
            protected set { type = value; }
        }

        public abstract Equipment Create(EquipmentTier tier);
        public abstract string[] GetTierSkillDescription();
    }
}
