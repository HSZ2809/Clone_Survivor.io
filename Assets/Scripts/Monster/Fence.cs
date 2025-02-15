using UnityEngine;

namespace ZUN
{
    public class Fence : MonoBehaviour, IMon_Damageable, IMon_Attackable, IMon_Destroyable
    {
        #region Inspector
        [Header("Status")]
        [SerializeField] float ap;

        [Header("EXP Type")]
        [SerializeField] private EXPShard.Type shardType;
        #endregion

        CircleCollider2D cc2D;
        Character character;
        EXPObjPool EXPPool;
        Animator anim;

        public float Ap { get => ap; set => ap = value; }

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            EXPPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<EXPObjPool>();
            tag = "Fence";
            gameObject.layer = LayerMask.NameToLayer("Wall");
            cc2D = GetComponent<CircleCollider2D>();
            anim = GetComponent<Animator>();
        }

        private void Start()
        {
            transform.parent = null;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Character"))
            {
                character.TakeDamage(ap);
            }
        }

        public void TakeDamage(float damage)
        {
            // 피격 시 시각 효과
            ShowDamage(damage);
        }

        public void ShowDamage(float damage)
        {
            
        }

        public void Die()
        {
            
        }
    }
}