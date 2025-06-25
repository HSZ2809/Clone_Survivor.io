using UnityEngine;

namespace ZUN
{
    public class Scroll : PassiveSkill
    {
        [SerializeField] float coefficient;
        [SerializeField] float addExpGain;

        private void Start()
        {
            character.SetPassiveSkill(this);
            // addExpGain = character.ExpGain * coefficient;
            character.UpgradeExpGain(addExpGain);
        }

        public override void Upgrade()
        {
            level += 1;

            if (level < 6)
                character.UpgradeExpGain(addExpGain);
            else
                Debug.LogWarning("Scroll Upgrade() : level exceeded");
        }
    }
}