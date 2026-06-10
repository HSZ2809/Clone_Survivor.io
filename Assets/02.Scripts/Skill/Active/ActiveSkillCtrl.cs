using UnityEngine;
using Zenject;

namespace ZUN
{
    public abstract class ActiveSkillCtrl : MonoBehaviour
    {
        protected Character character;
        [Inject] protected IManager_VisualEffect manager_VisualEffect;

        protected virtual void Awake()
        {
            character = FindFirstObjectByType<Character>();
        }

        public abstract void Upgrade(int level);
    }
}