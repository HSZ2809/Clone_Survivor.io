using UnityEngine;

namespace ZUN
{
    public abstract class ActiveSkill : Skill
    {
        protected Character character;
        protected Manager_Audio manager_Audio;
        [SerializeField] protected string synergyID;

        public string SynergyID { get { return synergyID; } }

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            manager_Audio = GameObject.FindObjectWithTag("Manager").GetComponent<Manager_Audio>();
            level = 1;
        }
    }
}
