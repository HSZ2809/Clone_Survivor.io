using UnityEngine;

namespace ZUN
{
    public class Manager_Inventory : MonoBehaviour
    {
        [SerializeField] ActiveSkill active;
        //[SerializeField] Armor armor;
        //[SerializeField] Necklace necklace;
        //[SerializeField] Belt belt;
        //[SerializeField] Glove gloves;
        //[SerializeField] Shoe shoes;

        public ActiveSkill Active { get { return active; } }

        private void Start()
        {
            DontDestroyOnLoad(this);
            Debug.Log("Manager_Inventory : create");
        }
    }
}