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
            order = character.AmountOfActive;
            level = 1;
        }

        public virtual void ActiveSkillOn() 
        {
            Debug.LogWarning("ActiveSkill : ActivateWeapon Mathod Not Set");
            return; 
        }

        public override void Upgrade()
        {
            Debug.LogWarning("ActiveSkill : Upgrade Mathod Not Set");
        }
    }
}