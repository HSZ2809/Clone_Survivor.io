using UnityEngine;

namespace ZUN
{
    public class PassiveSkill : Skill
    {
        protected Character character = null;

        private void Start()
        {
            character.SetPassiveSkill(this);
        }
    }
}