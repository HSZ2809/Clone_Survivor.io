using UnityEngine;

namespace ZUN
{
    public abstract class ActiveSkillCtrl : MonoBehaviour
    {
        protected Character character;
        //protected ActiveSkill activeSkill;
        protected Manager_VisualEffect manager_VisualEffect;

        protected virtual void Awake()
        {
            character = FindFirstObjectByType<Character>();
            manager_VisualEffect = FindFirstObjectByType<Manager_VisualEffect>();
        }

        public abstract void Upgrade(int level);
    }
}