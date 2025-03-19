using UnityEngine;

namespace ZUN
{
    public abstract class Skill : MonoBehaviour
    {
        protected Character character;

        public SkillInformation skillInfo;
        [SerializeField] protected int level;
        public int Level { get { return level; } }

        public abstract void Upgrade();
    }
}
