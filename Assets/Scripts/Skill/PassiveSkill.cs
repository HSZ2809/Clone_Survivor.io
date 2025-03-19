using UnityEngine;

namespace ZUN
{
    public abstract class PassiveSkill : Skill
    {
        // ScriptableObject 로 만들어진 ActiveSkillInfo 의 배열 필요

        private void Awake()
         {
             character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
             level = 1;
         }
    }
}
