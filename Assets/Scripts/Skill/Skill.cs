using UnityEngine;

namespace ZUN
{
    public abstract class Skill : MonoBehaviour
    {
        protected Character character;

        [SerializeField] protected SkillInformation skillInfo;
        [SerializeField] protected int level;

        public SkillInformation SkillInfo => skillInfo;
        public int Level => level;

        public abstract void Upgrade();
    }
}
