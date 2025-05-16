using UnityEngine;

namespace ZUN
{
    public abstract class SkillCtrl : MonoBehaviour
    {
        protected Character character;
        protected ActiveSkill activeSkill;
        protected Manager_VisualEffect manager_VisualEffect;

        protected virtual void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            manager_VisualEffect = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_VisualEffect>();
        }

        public abstract void Upgrade(int level);
    }
}