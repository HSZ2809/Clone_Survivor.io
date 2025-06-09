using UnityEngine;

namespace ZUN
{
    public class ActiveSkill : Skill
    {
        protected Manager_VisualEffect manager_VisualEffect;
        [SerializeField] protected string synergyID;
        [SerializeField] ActiveSkillCtrl[] prefab_skillCtrls = new ActiveSkillCtrl[2];
        const int nomal = 0, final = 1;

        ActiveSkillCtrl skillCtrl;

        public string SynergyID { get { return synergyID; } }

        protected override void Awake()
        {
            base.Awake();

            manager_VisualEffect = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_VisualEffect>();
        }

        private void Start()
        {
            skillCtrl = Instantiate(prefab_skillCtrls[nomal], transform);
            character.SetActiveSkill(this);
        }

        public override void Upgrade()
        {
            level += 1;

            if (level > SkillInfo.MaxLevel)
                return;

            if (level < SkillInfo.MaxLevel)
                skillCtrl.Upgrade(level);
            else
            {
                Destroy(skillCtrl.gameObject);
                skillCtrl = Instantiate(prefab_skillCtrls[final], transform);
                skillCtrl.Upgrade(level);
            }
        }
    }
}