using UnityEngine;

namespace ZUN
{
    public abstract class ActiveSkill : Skill
    {
        [SerializeField] protected Character character;
        [SerializeField] protected string synergyID;

        [SerializeField] public string SynergyID { get { return synergyID; } }

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            level = 1;
        }

        public abstract void ActiveSkillOn();
    }
}