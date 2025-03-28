using UnityEngine;

namespace ZUN
{
    [CreateAssetMenu(fileName = "SkillItemData", menuName = "Scriptable Objects/SkillItemData")]
    public class SkillItemData : ScriptableObject
    {
        [SerializeField] int           id;
        [SerializeField] string        itemName;
        [SerializeField] string        comment;
        [SerializeField] Sprite        iconSprite;
        //[SerializeField] int           maxLevel;
        [SerializeField] EquipmentType type;

        public int           Id         => id;
        public string        ItemName   => itemName;
        public string        Comment    => comment;
        public Sprite        IconSprite => iconSprite;
        //public int           MaxLevel   => maxLevel;
        public EquipmentType Type       => type;

        public enum EquipmentType
        {
            Weapom;
            Cloth;
            Necklace;
            Belt;
            Gloves;
            Shoes;
        }
    }
}
