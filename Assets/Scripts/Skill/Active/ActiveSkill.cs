using UnityEngine;

namespace ZUN
{
    public abstract class ActiveSkill : Skill
    {
        protected Manager_VisualEffect manager_VisualEffect;
        // synergyID -> ScriptableObject 로 교환 필요
        [SerializeField] protected string synergyID;
        public string SynergyID { get { return synergyID; } }

        protected override void Awake()
        {
            base.Awake();

            manager_VisualEffect = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_VisualEffect>();
        }
    }
}