using UnityEngine;

namespace ZUN
{
    public class Monster : MonoBehaviour
    {
        [Header("EXP Prefab")]
        [SerializeField] private DropedEXP dropedEXP;

        [Header("Status")]
        [SerializeField] private float hp = 0.0f;
        [SerializeField] private float attackPower = 0.0f;
        [SerializeField] private float moveSpeed = 0.0f;

        [Header("Character Transform")]
        [SerializeField] private Transform character = null;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Transform>();
        }

        private void Update()
        {
            Vector3 moveDirection = (character.position - transform.position).normalized;
            moveDirection *= moveSpeed;

            transform.Translate(moveDirection * Time.deltaTime);
        }

        public float Hp
        {
            get { return hp; }
            set { hp += value; }
        }

        public float AttackPower
        {
            get { return attackPower; }
        }

        public void Hit(float damage)
        {
            hp -= damage;
            Debug.Log("Monster : hit! - damage : " + damage);

            if (hp < 0)
            {
                Instantiate(dropedEXP, transform.position, transform.rotation);
                gameObject.SetActive(false);
                Debug.Log("Monster : Died");
            }
        }
    }
}