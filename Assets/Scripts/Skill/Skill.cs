using UnityEngine;

namespace ZUN
{
    public abstract class Skill : MonoBehaviour
    {
        [Header("Skill Info")]
        [SerializeField] protected string id;
        [SerializeField] protected string skillName;
        [SerializeField] protected SkillType type;
        [SerializeField] protected Sprite sprite;
        [SerializeField] protected string[] upgradeInfos;
        [SerializeField] protected int level;
        [SerializeField] protected int maxLevel;

        public string ID { get { return id; } }
        public string SkillName { get { return skillName; } }
        public SkillType Type { get { return type; } }
        public Sprite Sprite { get { return sprite; } }
        public string[] UpgradeInfos { get { return upgradeInfos; } }
        public int Level { get { return level; } }
        public int MaxLevel { get { return maxLevel; } }

        public abstract void Upgrade();

        public enum SkillType { ACTIVE = 0, PASSIVE  = 1 };
    }
}
