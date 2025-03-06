using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class Mon_Shooter : Monster, IMon_Movement, IMon_Damageable, IMon_Attackable, IMon_Destroyable, IMon_ShootBullet, IMon_KnockBackable
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

        EXPObjPool EXPPool;
        Rigidbody2D rb;
        Animator anim;
        IObjectPool<MonsterBullet> bulletPool;
        readonly WaitForSeconds waitTime = new (10.0f);

        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float MaxHp { get; set; }
        public float Ap { get => ap; set => ap = value; }

        private void Awake()
        {
            chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ChapterCtrl>();
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            EXPPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<EXPObjPool>();
            bulletPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<MonsterBulletManager>().BulletPool;
            tag = "Monster";
            gameObject.layer = LayerMask.NameToLayer(tag);
            cc2D = GetComponent<CircleCollider2D>();
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
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
                gameObject.SetActive(false);

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
            transform.position = Vector3.MoveTowards(transform.position, character.transform.position, Time.deltaTime * moveSpeed);
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

        public void TakeDamage(float damage)
        {
            hp -= damage;
            ShowDamage(damage);

            if (hp <= 0)
            {
                cc2D.enabled = false;
                StopCoroutine(Shoot());
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
            StopCoroutine(Shoot());
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