using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class Mon_Shooter : NomalMonster, IMovement, IDamageable, IAttackable, IShootBullet, IKnockBackable, IBleeding
    {
        #region Inspector
        [Header("Status")]
        [SerializeField] float hp;
        [SerializeField] float ap;
        [SerializeField] float moveSpeed;
        [SerializeField] float limitDistance;

        [Header("Bullet")]
        [SerializeField] MonsterBullet bullet;

        [Header("Sprite Renderer")]
        [SerializeField] SpriteRenderer sr;

        [Header("EXP Type")]
        [SerializeField] EXPShard.Type shardType;
        #endregion

        float slowMultiplier = 1.0f;

        ObjectPool_ExpShard EXPPool;
        ObjectPool_DamageText damageTextPool;
        Rigidbody2D rb;
        Animator anim;
        ParticleSystem bleeding;
        IObjectPool<MonsterBullet> bulletPool;
        readonly WaitForSeconds waitTime = new (10.0f);

        public float BaseMoveSpeed => moveSpeed;
        public float CurrentMoveSpeed => moveSpeed * slowMultiplier;
        public float MaxHp { get; set; }
        public float Ap { get => ap; set => ap = value; }

        private void Awake()
        {
            chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ChapterCtrl>();
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            EXPPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ObjectPool_ExpShard>();
            bulletPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<MonsterBulletManager>().BulletPool;
            damageTextPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ObjectPool_DamageText>();
            SetTagAndLayer();
            cc2D = GetComponent<CircleCollider2D>();
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            bleeding = GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            hp = MaxHp;
            cc2D.enabled = true;
            StartCoroutine(Shoot());
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, character.transform.position) > limitDistance)
                Release();

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
            ap = _ap;
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

        public void ShootBullet()
        {
            // 발사체 발사 로직
        }

        IEnumerator Shoot()
        {
            while (true)
            {
                yield return waitTime;

                Vector3 aim = (transform.position - character.transform.position).normalized;
                float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
                MonsterBullet bullet = bulletPool.Get();
                bullet.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, angle + 90));
                bullet.Damage = Ap;
                bullet.gameObject.SetActive(true);
            }
        }

        public bool TryTakeDamage(float damage)
        {
            hp -= damage;
            ShowDamage(damage);

            if (hp <= 0)
            {
                cc2D.enabled = false;
                StopCoroutine(Shoot());
                Die();
            }

            return hp <= 0;
        }

        public void ShowDamage(float damage)
        {
            DamageText damageText = damageTextPool.Pool.Get();
            damageText.transform.position = transform.position;
            damageText.SetText(damage);
        }

        public void DropShard()
        {
            EXPPool.SetShard(shardType, transform.position);
            chapterCtrl.AddKillCount();
        }

        public void Die()
        {
            StopCoroutine(Shoot());
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