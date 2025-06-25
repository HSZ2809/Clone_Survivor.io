using UnityEngine;

namespace ZUN
{
    public class Exoskeleton : PassiveSkill
    {
        [SerializeField] float coefficient;
        [SerializeField] float addDuration;

        private void Start()
        {
            character.SetPassiveSkill(this);
            addDuration = character.Duration * coefficient;
            character.UpgradeDuration(addDuration);
        }

        public override void Upgrade()
        {
            level += 1;

            if (level < 6)
                character.UpgradeDuration(addDuration);
            else
                Debug.LogWarning("Exoskeleton Upgrade() : level exceeded");
        }
    }
}