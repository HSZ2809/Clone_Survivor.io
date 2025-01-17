using UnityEngine;

namespace ZUN
{
    public class PassiveSkill : Skill
    {
        [SerializeField] protected Character character = null;

        private void Start()
        {
            character.SetPassive(this);
        }
    }
}