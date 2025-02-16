using UnityEngine;

namespace ZUN
{
    public class BossSpawn : MonoBehaviour
    {
        Character character;

        [Header("Boss")]
        [SerializeField] Monster boss;
        [SerializeField] float hp;
        [SerializeField] float attackPower;

        //[Header("Range")]
        //[SerializeField] float disistance = 16.45f;

        Animator animator;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            animator = GetComponent<Animator>();
        }
        
        public void SetReady()
        {
            transform.parent = null;
            animator.SetTrigger("ready");
        }

        private void SetBoss()
        {
            // Vector2 setLoc = character.transform.position + new Vector3(0, disistance);
            Monster mon = Instantiate(boss, transform.position, transform.rotation);
            mon.SetMonsterSpec(hp, attackPower);
            mon.gameObject.SetActive(true);
        }

        private void DestroyObject()
        {
            Destroy(this);
        }
    }
}