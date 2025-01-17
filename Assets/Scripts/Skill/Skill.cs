using UnityEngine;

namespace ZUN
{
    public abstract class Skill : MonoBehaviour
    {
        [Header("Skill Info")]
        [SerializeField] protected string id;
        [SerializeField] protected string skillName;
        [SerializeField] protected Sprite sprite;
        [SerializeField] protected string[] upgradeInfos;
        [SerializeField] protected int level = 1;

        public string ID { get { return id; } }
        public string SkillName { get { return skillName; } }
        public Sprite Sprite { get { return sprite; } }
        public string[] UpgradeInfos { get { return upgradeInfos; } }
        public int Level { get { return level; } }

        public virtual void Upgrade() 
        {
            Debug.LogWarning("Skill : Upgrade Mathod Not Set");
        }
    }
}