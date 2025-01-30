using UnityEngine;

namespace ZUN
{
    public class PassiveSkill : Skill
    {
        protected Character character = null;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            level = 1;
        }
    }
}