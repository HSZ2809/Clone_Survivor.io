using UnityEngine;

namespace ZUN
{
    public class Passive_05 : PassiveSkill
    {
        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        }
    }
}
