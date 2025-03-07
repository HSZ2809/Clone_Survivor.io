using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Bouncebloom : BossMonster, IMon_Damageable, IMon_Attackable, IMon_Destroyable, IMon_ShootBullet
    {
        #region Inspector
        [Header("Status")]
        [SerializeField] float hp;
        [SerializeField] float ap;

        [Header("Sprite Renderer")]
        [SerializeField] SpriteRenderer sr;

        [Header("Treasure Box")]
        [SerializeField] TreasureBox treasureBox;

        [Header("Boss Pattern")]
        [SerializeField] Boss_Bouncebloom_Bullet bullet;
        #endregion

        [SerializeField] Animator anim;
        readonly WaitForSeconds waitTime = new(3);

        public float MaxHp { get; set; }
        public float Ap { get => ap; set => ap = value; }

        private void Awake()
        {
            tag = "Monster";
            gameObject.layer = LayerMask.NameToLayer(tag);
            cc2D = GetComponent<CircleCollider2D>();
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ChapterCtrl>();
        }

        private void OnEnable()
        {
            ShootBullet();
        }

        private void Update()
        {
            if (character.transform.position.x > transform.position.x)
                sr.flipX = false;
            else
                sr.flipX = true;
        }

        public override void SetMonsterSpec(float _maxHp, float _ap)
        {
            MaxHp = _maxHp;
            Ap = _ap;

            hp = MaxHp;
            ap = Ap;
        }

        public void ShootBullet()
        {
            StartCoroutine(SetIdle());
        }

        IEnumerator SetIdle()
        {
            yield return waitTime;

            int randomPattern;
            randomPattern = Random.Range(1, 3);

            switch(randomPattern)
            {
                case 1:
                    StartCoroutine(Pattern_1());
                    break;

                case 2:
                    StartCoroutine(Pattern_2());
                    break;
            }
        }

        IEnumerator Pattern_1()
        {
            for(int i = 0; i < 3; i++)
            {
                Boss_Bouncebloom_Bullet mb = Instantiate(bullet);
                mb.transform.position = transform.position;
                mb.Direction = (character.transform.position - transform.position).normalized;
                mb.gameObject.SetActive(true);

                yield return new WaitForSeconds(0.5f);
            }

            StartCoroutine(SetIdle());
        }

        IEnumerator Pattern_2()
        {
            for (int i = -1; i < 2; i++)
            {
                Boss_Bouncebloom_Bullet mb = Instantiate(bullet);
                mb.transform.position = transform.position;
                mb.Direction = Quaternion.Euler(0, 0, i * 15) * (character.transform.position - transform.position).normalized;
                mb.gameObject.SetActive(true);
            }

            StartCoroutine(SetIdle());
            yield return null;
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
            hp -= damage;
            bossHpBar.fillAmount = hp / MaxHp;
            ShowDamage(damage);

            if (hp <= 0)
            {
                cc2D.enabled = false;
                StopCoroutine(SetIdle());
                bossHpUI.SetActive(false);
                anim.SetTrigger("Die");
            }
        }

        public void ShowDamage(float damage)
        {
            // 데미지 표시 로직
        }

        public void DropTreasureBox()
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