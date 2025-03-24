using UnityEngine;

namespace ZUN
{
    public abstract class ActiveSkill : Skill
    {
        // synergyID -> ScriptableObject 로 교환 필요
        [SerializeField] protected string synergyID;
        public string SynergyID { get { return synergyID; } }
    }
}
