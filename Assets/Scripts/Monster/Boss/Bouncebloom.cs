using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Bouncebloom : Monster, IMon_Damageable, IMon_Attackable, IMon_Destroyable, IMon_ShootBullet
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
        [SerializeField] MonsterBullet bullet;
        #endregion

        [SerializeField] Animator anim;
        readonly WaitForSeconds waitTime = new(5);

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
            Fire();
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

        public void Fire()
        {
            StartCoroutine(Pattern_Shoot());
        }

        IEnumerator Pattern_Shoot()
        {
            yield return waitTime;

            Vector3 aim = (transform.position - character.transform.position).normalized;
            float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            MonsterBullet mb = Instantiate(bullet);
            mb.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, angle + 90));
            mb.gameObject.SetActive(true);
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
            ShowDamage(damage);

            if (hp <= 0)
            {
                cc2D.enabled = false;
                StopCoroutine(Pattern_Shoot());
                anim.SetTrigger("Die");
            }
        }

        public void ShowDamage(float damage)
        {
            // 데미지 표시 로직
        }

        public void DropTreasureBox()
        {
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