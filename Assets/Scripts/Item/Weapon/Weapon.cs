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

        public virtual void ActivateWeapon() 
        {
            Debug.LogWarning("Weapon : ActivateWeapon Mathod Not Set");
            return; 
        }

        public override bool TryUpgrade(int level)
        {
            Debug.LogWarning("Weapon : Upgrade Mathod Not Set");
            return false;
        }
    }
}