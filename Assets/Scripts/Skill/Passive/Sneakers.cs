using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Sneakers : PassiveSkill
    {
        [SerializeField] float coefficient;
        [SerializeField] float addMoveSpeed;

        private void Start()
        {
            character.SetPassiveSkill(this);
            addMoveSpeed = character.MoveSpeed * coefficient;
            character.UpgradeMoveSpeed(addMoveSpeed);
        }

        public override void Upgrade()
        {
            level += 1;

            if (level < 6)
                character.UpgradeMoveSpeed(addMoveSpeed);
            else
                Debug.LogWarning("Sneakers Upgrade() : level exceeded");
        }
    }
}