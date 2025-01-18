using UnityEngine;

namespace ZUN
{
    public abstract class ActiveSkill : Skill
    {
        [SerializeField] protected Character character;
        [SerializeField] protected string synergyID;

        [SerializeField] public string SynergyIN { get { return synergyID; } }

        private void Start()
        {
            character.SetActiveSkill(this);
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