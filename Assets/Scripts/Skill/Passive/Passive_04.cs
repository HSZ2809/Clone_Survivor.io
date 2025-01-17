using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Passive_04 : PassiveSkill
    {
        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        }
    }
}