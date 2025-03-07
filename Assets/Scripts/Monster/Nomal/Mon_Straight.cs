using DG.Tweening;
using UnityEngine;

namespace ZUN
{
    public class Mon_Straight : Monster, IMon_Movement, IMon_Damageable, IMon_Attackable, IMon_Destroyable, IMon_KnockBackable
    {
        #region Inspector
        [SerializeField] float hp;
        [SerializeField] float ap;
        [SerializeField] float moveSpeed;
        [SerializeField] float limitDistance;

        [SerializeField] Animator anim;

        [Header("Sprite Renderer")]
        [SerializeField] private SpriteRenderer sr;

        [Header("EXP Type")]
        [SerializeField] private EXPShard.Type shardType;
        #endregion

        EXPObjPool EXPPool;
        Rigidbody2D rb;
        Vector2 moveDirection;

        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float MaxHp { get; set; }
        public float Ap { get => ap; set => ap = value; }

        private void Awake()
        {
            chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ChapterCtrl>();
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            EXPPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<EXPObjPool>();
            tag = "Monster";
            gameObject.layer = LayerMask.NameToLayer(tag);
            cc2D = GetComponent<CircleCollider2D>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            hp = MaxHp;
            cc2D.enabled = true;
            moveDirection = (character.transform.position - transform.position).normalized;

            if (character.transform.position.x > transform.position.x)
                sr.flipX = false;
            else
                sr.flipX = true;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, character.transform.position) > limitDistance)
                gameObject.SetActive(false);

            if (hp > 0)
                Move();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet"))
            // if (!DOTween.IsTweening(sr.gameObject.transform))
            {
                sr.gameObject.transform.DORewind();
                sr.gameObject.transform.DOShakeScale(0.3f, 1, 3, 0).Restart();
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Character"))
            {
                character.TakeDamage(ap);
            }
        }

        public override void SetMonsterSpec(float _maxHp, float _ap)
        {
            MaxHp = _maxHp;
            ap = _ap;
        }

        public void Move()
        {
            transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
        }

        public void TakeDamage(float damage)
        {
            hp -= damage;
            ShowDamage(damage);

            if (hp <= 0)
            {
                cc2D.enabled = false;
                Die();
            }
        }

        public void ShowDamage(float damage)
        {
            // 데미지 표시 로직
        }

        public void DropShard()
        {
            EXPPool.SetShard(shardType, transform.position);
            chapterCtrl.AddKillCount();
        }

        public void Die()
        {
            anim.SetTrigger("Die");
        }

        public void KnockBack()
        {
            rb.AddForce((transform.position - character.transform.position).normalized * moveSpeed, ForceMode2D.Impulse);
        }

        void Release()
        {
            sr.gameObject.transform.DOKill();
            sr.gameObject.transform.DORewind();
            gameObject.SetActive(false);
        }
    }
}