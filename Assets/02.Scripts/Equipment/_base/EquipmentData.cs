using UnityEngine;

namespace ZUN
{
    public enum EquipmentType
    {
        Weapon,
        Armor,
        Necklace,
        Belt,
        Gloves,
        Shoes
    }

    public abstract class EquipmentData : ScriptableObject
    {
        [SerializeField] string id;
        public string Id => id;
        [SerializeField] string equipmentName;
        public string EquipmentName => equipmentName;
        [TextArea(2, 3)]
        [SerializeField] string comment;
        public string Comment => comment;
        [SerializeField] Sprite[] iconSprite;
        public Sprite[] IconSprite => iconSprite;
        [SerializeField] int[] maxLevel;
        public int[] MaxLevel   => maxLevel;
        [SerializeField] EquipmentType type;
        public EquipmentType Type 
        { 
            get {  return type; } 
            protected set { type = value; }
        }
        [SerializeField] int[] initialStat;
        public int[] InitialStat => initialStat;
        [SerializeField] int[] increaseStatPerLevel;
        public int[] IncreaseStatPerLevel => increaseStatPerLevel;
        [SerializeField] float[] coefficient;
        public float[] Coefficient => coefficient;

        public abstract Equipment Create(string uuid, EquipmentTier tier, int level);
        public abstract string[] GetTierSkillDescription();
    }
}
