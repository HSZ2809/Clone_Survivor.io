using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class CarionCrawler : BossMonster, IMovement, IDamageable, IAttackable, IDestroyable, IBleeding
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

        float slowMultiplier = 1.0f;

        IObjectPool<MonsterBullet> bulletPool;
        ObjectPool_DamageText damageTextPool;
        ParticleSystem bleeding;
        BGMCtrl bgmCtrl;

        readonly Vector2 originX = new (1, 1);
        readonly Vector2 flipX = new (-1, 1);

        public float BaseMoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float CurrentMoveSpeed => moveSpeed * slowMultiplier;
        public float MaxHp { get; set; }
        public float Ap { get => ap; set => ap = value; }

        bool state_Move = true;

        private void Awake()
        {
            SetTagAndLayer();
            cc2D = GetComponent<CircleCollider2D>();
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            GameObject chapterCtrlObj = GameObject.FindGameObjectWithTag("ChapterCtrl");
            if (chapterCtrlObj != null)
            {
                chapterCtrl = chapterCtrlObj.GetComponent<ChapterCtrl>();
                bulletPool = chapterCtrlObj.GetComponent<MonsterBulletManager>().BulletPool;
                damageTextPool = chapterCtrlObj.GetComponent<ObjectPool_DamageText>();
                timer = chapterCtrlObj.GetComponent<Timer>();
                chapterCtrlObj.TryGetComponent<BGMCtrl>(out bgmCtrl);
                chapterCtrlObj.TryGetComponent<TimeLineCtrl>(out timeLineCtrl);
            }
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

        void StartPattern_Rush()
        {
            StartCoroutine(Rush());
        }

        IEnumerator Rush()
        {
            Vector2 diraction = (character.transform.position - transform.position).normalized;
            float rushTime = 0.0f;

            while (rushTime < 1.5f)
            {
                transform.Translate(8 * moveSpeed * Time.deltaTime * diraction);

                rushTime += Time.deltaTime;
                yield return null;
            }
        }

        void ShootBullet()
        {
            MonsterBullet _bullet = bulletPool.Get();
            Vector3 aim = (shootPos.position - character.transform.position).normalized;
            float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            _bullet.transform.SetPositionAndRotation(shootPos.position, Quaternion.Euler(0, 0, angle + 90));
            _bullet.gameObject.SetActive(true);
        }

        void DropTreasureBox()
        {
            StopAllCoroutines();
            Instantiate(treasureBox, transform.position, transform.rotation);
            chapterCtrl.AddKillCount();
        }

        void SetMoveTure()
        {
            state_Move = true;
        }

        void SetMoveFalse()
        {
            state_Move = false;
        }

        void SelectPattern()
        {
            int random = Random.Range(1, 3);

            switch(random)
            {
                case 1:
                    Toggle_Shoot();
                    break;
                case 2:
                    Toggle_Rush();
                    break;
            }
        }

        void Toggle_Shoot()
        {
            anim.SetBool("Shoot", !anim.GetBool("Shoot"));
        }

        void Toggle_Rush()
        {
            anim.SetBool("Rush", !anim.GetBool("Rush"));
        }

        public void Die()
        {
            timer.PauseTimer = false;
            bgmCtrl.SetDefaultClip();
            timeLineCtrl.Play();
            Destroy(gameObject);
        }
    }
}