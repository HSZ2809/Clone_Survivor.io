using UnityEngine;

namespace ZUN
{
    public abstract class Weapon : Item
    {
        [SerializeField] protected Character character = null;

        private void Start()
        {
            character.RegistrationWeapon(this);
        }

        public virtual void ActivateWeapon(float attackSpeed)
        {

        }
    }
}