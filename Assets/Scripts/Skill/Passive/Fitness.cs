using UnityEngine;

namespace ZUN
{
    public class Fitness : PassiveSkill
    {
        [SerializeField] float coefficient;
        [SerializeField] float addMaxHp;

        private void Start()
        {
            character.SetPassiveSkill(this);
            addMaxHp = character.MaxHp * coefficient;
            character.UpgradeMaxHp(addMaxHp);
        }

        public override void Upgrade()
        {
            level += 1;

            if (level < 6)
                character.UpgradeMaxHp(addMaxHp);
            else
                Debug.LogWarning("Fitness Upgrade() : level exceeded");
        }
    }
}
