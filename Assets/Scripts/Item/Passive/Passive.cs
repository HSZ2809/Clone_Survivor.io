using UnityEngine;

namespace ZUN
{
    public class Passive : Item
    {
        [SerializeField] protected Character character = null;

        private void Start()
        {
            character.RegistrationPassive(this);
        }
    }
}