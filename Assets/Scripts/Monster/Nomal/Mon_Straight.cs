using DG.Tweening;
using UnityEngine;

namespace ZUN
{
    public class Mon_Straight : NomalMonster, IMovement, IDamageable, IAttackable, IDestroyable, IKnockBackable, IBleeding
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

        ObjectPool_ExpShard EXPPool;
        ObjectPool_DamageText damageTextPool;
        Rigidbody2D rb;
        ParticleSystem bleeding;
        Vector2 moveDirection;

        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float MaxHp { get; set; }
        public float Ap { get => ap; set => ap = value; }

        private void Awake()
        {
            chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ChapterCtrl>();
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            EXPPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ObjectPool_ExpShard>();
            damageTextPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ObjectPool_DamageText>();
            SetTagAndLayer();
            cc2D = GetComponent<CircleCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            bleeding = GetComponent<ParticleSystem>();
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

            float randomValue = Random.Range(-1, 1);
            rb.AddForce(new Vector2(randomValue, randomValue));
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, character.transform.position) > limitDistance)
                Release();

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

        public float TakeDamage(float damage)
        {
            hp -= damage;
            ShowDamage(damage);

            if (hp <= 0)
            {
                cc2D.enabled = false;
                Die();
            }

            return hp;
        }

        public void ShowDamage(float damage)
        {
            DamageText damageText = damageTextPool.Pool.Get();
            damageText.transform.position = transform.position;
            damageText.SetText(damage.ToString());
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

        public void Bleeding()
        {
            bleeding.Play();
        }

        void Release()
        {
            sr.gameObject.transform.DOKill();
            sr.gameObject.transform.DORewind();
            gameObject.SetActive(false);
            monsterSpawner.Release(this);
        }
    }
}