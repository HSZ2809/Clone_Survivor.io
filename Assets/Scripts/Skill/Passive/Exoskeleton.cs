using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Exoskeleton : PassiveSkill
    {
        [SerializeField] float coefficient;
        [SerializeField] float addDuration;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
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