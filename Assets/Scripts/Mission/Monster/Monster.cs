using UnityEngine;

namespace ZUN
{
    public abstract class Monster : MonoBehaviour
    {
        [Header("Status")]
        [SerializeField] protected float hp = 0.0f;
        [SerializeField] protected float attackPower = 0.0f;
        [SerializeField] protected float moveSpeed = 0.0f;
        protected float speed;

        protected Transform character = null;


        private void Awake()
        {
            tag = "Monster";
            gameObject.layer = LayerMask.NameToLayer(tag);
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Transform>();
        }

        public float Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        public float AttackPower
        {
            get { return attackPower; }
        }

        public virtual void Hit(float damage)
        {
            hp -= damage;

            if (hp < 0)
                gameObject.SetActive(false);
        }
    }
}