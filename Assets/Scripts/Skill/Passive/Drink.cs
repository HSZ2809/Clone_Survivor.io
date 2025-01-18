using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Drink : PassiveSkill
    {
        [SerializeField] float regeneration;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            character.UpgradeRegeneration(regeneration);
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