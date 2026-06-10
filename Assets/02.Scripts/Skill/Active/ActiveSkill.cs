using UnityEngine;
using Zenject;

namespace ZUN
{
    public class ActiveSkill : Skill
    {
        [Inject] private DiContainer _container;

        [SerializeField] protected string synergyID;
        [SerializeField] ActiveSkillCtrl[] prefab_skillCtrls = new ActiveSkillCtrl[2];
        const int NOMAL = 0, ENHANCE = 1;

        ActiveSkillCtrl skillCtrl;

        public string SynergyID { get { return synergyID; } }

        private void Start()
        {
            skillCtrl = _container.InstantiatePrefab(prefab_skillCtrls[NOMAL], transform)
                .GetComponent<ActiveSkillCtrl>();
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
                skillCtrl = _container.InstantiatePrefab(prefab_skillCtrls[ENHANCE], transform)
                    .GetComponent<ActiveSkillCtrl>();
                skillCtrl.Upgrade(level);
            }
        }
    }
}