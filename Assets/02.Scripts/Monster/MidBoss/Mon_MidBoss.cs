using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Mon_MidBoss : Monster, IMovement, IDamageable, IAttackable, IBleeding
    {
        #region Inspector
        [Header("Status")]
        [SerializeField] float hp;
        [SerializeField] float ap;
        [SerializeField] float moveSpeed;

        [SerializeField] Animator anim;

        [Header("Sprite Renderer")]
        [SerializeField] private SpriteRenderer sr;

        [Header("Treasure Box")]
        [SerializeField] private TreasureBox treasureBox;
        #endregion

        float slowMultiplier = 1.0f;

        ObjectPool_DamageText damageTextPool;
        ParticleSystem bleeding;

        public float BaseMoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float CurrentMoveSpeed => moveSpeed * slowMultiplier;
        public float MaxHp { get; set; }
        public float Ap { get => ap; set => ap = value; }

        private void Awake()
        {
            SetTagAndLayer();
            cc2D = GetComponent<CircleCollider2D>();
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ChapterCtrl>();
            damageTextPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ObjectPool_DamageText>();
            bleeding = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (character.transform.position.x > transform.position.x)
                sr.flipX = false;
            else
                sr.flipX = true;

            if (hp > 0)
                Move();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet"))
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
            Ap = _ap;

            hp = MaxHp;
            ap = Ap;
        }
        public void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, character.transform.position, Time.deltaTime * CurrentMoveSpeed);
        }

        public void ApplySlowEffect(float slowMultiplier)
        {
            StartCoroutine(GetSlowEffect(slowMultiplier));
        }

        IEnumerator GetSlowEffect(float slowMultiplier)
        {
            this.slowMultiplier = slowMultiplier;

            yield return new WaitForSeconds(1.0f);

            this.slowMultiplier = 1.0f;
        }

        public bool TryTakeDamage(float damage)
        {
            hp -= damage;
            ShowDamage(damage);

            if (hp <= 0)
            {
                cc2D.enabled = false;
                anim.SetTrigger("Die");
            }

            return hp <= 0;
        }

        public void ShowDamage(float damage)
        {
            DamageText damageText = damageTextPool.Pool.Get();
            damageText.transform.position = transform.position;
            damageText.SetText(damage);
        }

        public void Bleeding()
        {
            bleeding.Play();
        }

        void DropTreasureBox()
        {
            Instantiate(treasureBox, transform.position, transform.rotation);
            chapterCtrl.AddKillCount();
        }

        public void Die()
        {
            anim.SetTrigger("Die");
        }

        void Release()
        {
            gameObject.SetActive(false);
        }
    }
}