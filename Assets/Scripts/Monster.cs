using UnityEditor.U2D.Aseprite;
using UnityEditor.UIElements;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace ZUN
{
    public abstract class Monster : MonoBehaviour
    {
        [Header("EXP Prefab")]
        [SerializeField] protected DropedEXP dropedEXP;

        [Header("Status")]
        [SerializeField] protected float hp = 0.0f;
        [SerializeField] protected float attackPower = 0.0f;
        [SerializeField] protected float moveSpeed = 0.0f;

        [Header("Character Transform")]
        [SerializeField] protected Transform character = null;


        private void Awake()
        {
            tag = "Monster";
            gameObject.layer = LayerMask.NameToLayer(tag);
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Transform>();
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