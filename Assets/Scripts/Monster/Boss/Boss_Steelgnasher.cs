using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Boss_Steelgnasher : BossMonster, IMovement, IDamageable, IAttackable, IDestroyable, IBleeding
    {
        #region Inspector
        [SerializeField] float hp;
        [SerializeField] float ap;
        [SerializeField] float moveSpeed;

        [SerializeField] Animator anim;

        [Header("Sprite object")]
        [SerializeField] GameObject sprites;

        [Header("Treasure Box")]
        [SerializeField] TreasureBox treasureBox;

        [Header("Boss Pattern")]
        [SerializeField] Transform shootPos;
        [SerializeField] MonsterBullet bullet;
        #endregion

        ObjectPool_DamageText damageTextPool;
        ParticleSystem bleeding;

        readonly Vector2 originX = new(1, 1);
        readonly Vector2 flipX = new(-1, 1);

        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float MaxHp { get; set; }
        public float Ap { get => ap; set => ap = value; }

        bool state_Move = true;

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
            if (state_Move)
            {
                if (character.transform.position.x > transform.position.x)
                    sprites.transform.localScale = flipX;
                else
                    sprites.transform.localScale = originX;

                if (hp > 0)
                    Move();
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Character"))
            {
                character.TakeDamage(ap);
            }
        }

        void StartPattern_Rush()
        {
            StartCoroutine(Rush());
        }

        IEnumerator Rush()
        {
            float rushTime = 0.0f;

            while (rushTime < 1.0f)
            {
                // 화살표 표시

                if (character.transform.position.x > transform.position.x)
                    sprites.transform.localScale = flipX;
                else
                    sprites.transform.localScale = originX;

                rushTime += Time.deltaTime;
                yield return null;
            }

            Vector2 diraction = (character.transform.position - transform.position).normalized;

            rushTime = 0.0f;
            while (rushTime < 1.5f)
            {
                transform.Translate(8 * moveSpeed * Time.deltaTime * diraction);

                rushTime += Time.deltaTime;
                yield return null;
            }
        }

        void SetMoveTure()
        {
            state_Move = true;
        }

        void SetMoveFalse()
        {
            state_Move = false;
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
            transform.position = Vector3.MoveTowards(transform.position, character.transform.position, Time.deltaTime * moveSpeed);
        }

        public float TakeDamage(float damage)
        {
            hp -= damage;
            bossHpBar.fillAmount = hp / MaxHp;
            ShowDamage(damage);

            if (hp <= 0)
            {
                cc2D.enabled = false;
                bossHpUI.SetActive(false);
                anim.SetTrigger("Die");
            }

            return hp;
        }

        public void ShowDamage(float damage)
        {
            DamageText damageText = damageTextPool.Pool.Get();
            damageText.transform.position = transform.position;
            damageText.SetText(damage.ToString());
        }

        public void Bleeding()
        {
            bleeding.Play();
        }

        private void ShootBullet()
        {
            
        }

        private void DropTreasureBox()
        {
            StopAllCoroutines();
            Instantiate(treasureBox, transform.position, transform.rotation);
            chapterCtrl.AddKillCount();
        }

        public void Die()
        {
            chapterCtrl.PauseTimer = false;
            Destroy(gameObject);
        }
    }
}