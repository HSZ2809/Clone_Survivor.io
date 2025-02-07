using UnityEngine;

namespace ZUN
{
    public class Mon_Straight : Monster
    {
        #region Inspector
        [SerializeField] float currentHp;
        [SerializeField] float moveSpeed;

        [SerializeField] Animator anim;

        [Header("Sprite Renderer")]
        [SerializeField] private SpriteRenderer sr;

        [Header("EXP Type")]
        [SerializeField] private EXPShard.Type shardType;
        #endregion

        EXPObjPool EXPPool;
        Vector2 moveDirection;

        private void Awake()
        {
            chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ChapterCtrl>();
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            EXPPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<EXPObjPool>();
            tag = "Monster";
            gameObject.layer = LayerMask.NameToLayer(tag);
            cc2D = GetComponent<CircleCollider2D>();
        }

        private void OnEnable()
        {
            currentHp = hp;
            cc2D.enabled = true;
            moveDirection = (character.transform.position - transform.position).normalized;

            if (character.transform.position.x > transform.position.x)
                sr.flipX = false;
            else
                sr.flipX = true;
        }

        private void Update()
        {
            if (currentHp > 0)
                transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Character"))
            {
                character.Hit(attackPower);
            }
        }

        public override void Hit(float damage)
        {
            currentHp -= damage;

            if (currentHp <= 0)
            {
                cc2D.enabled = false;
                anim.SetTrigger("Die");
            }
        }

        public void DropShard()
        {
            EXPPool.SetShard(shardType, transform.position);
            chapterCtrl.AddKillCount();
        }

        public void Die()
        {
            gameObject.SetActive(false);
        }
    }
}