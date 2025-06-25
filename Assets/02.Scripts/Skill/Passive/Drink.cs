using UnityEngine;

namespace ZUN
{
    public class Drink : PassiveSkill
    {
        [SerializeField] float regeneration;

        private void Start()
        {
            character.SetPassiveSkill(this);
            character.UpgradeRegeneration(regeneration);
            character.StartRegenerationHp();
        }

        public override void Upgrade()
        {
            level += 1;

            if(level < 6)
                character.UpgradeRegeneration(regeneration);
            else
                Debug.LogWarning("Drink Upgrade() : level exceeded");
        }
    }
}
