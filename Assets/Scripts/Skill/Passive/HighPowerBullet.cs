using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class HighPowerBullet : PassiveSkill
    {
        [SerializeField] float coefficient;
        [SerializeField] float addAtk;

        private void Start()
        {
            character.SetPassiveSkill(this);
            addAtk = character.Atk * coefficient;
            character.UpgradeAtk(addAtk);
        }

        public override void Upgrade()
        {
            level += 1;

            if (level < 6)
                character.UpgradeAtk(addAtk);
            else
                Debug.LogWarning("HighPowerBullet Upgrade() : level exceeded");
        }
    }
}