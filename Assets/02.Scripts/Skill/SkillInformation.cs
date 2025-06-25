using UnityEngine;

namespace ZUN
{
    [CreateAssetMenu(fileName = "SkillInformation", menuName = "Scriptable Objects/SkillInformation")]
    public class SkillInformation : ScriptableObject
    {
        public enum SkillType { ACTIVE = 0, PASSIVE = 1 };

        [Header("Skill Info")]
        [SerializeField] string id;
        [SerializeField] string skillName;
        [SerializeField] SkillType type;
        [SerializeField] Sprite[] sprite;
        [SerializeField] string[] upgradeInfos;
        [SerializeField] int maxLevel;

        public string ID { get { return id; } }
        public string SkillName { get { return skillName; } }
        public SkillType Type { get { return type; } }
        public Sprite[] Sprite { get { return sprite; } }
        public string[] UpgradeInfos { get { return upgradeInfos; } }
        public int MaxLevel { get { return maxLevel; } }
    }
}