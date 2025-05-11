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

        protected virtual void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            level = 1;
        }

        public abstract void Upgrade();
    }
}
