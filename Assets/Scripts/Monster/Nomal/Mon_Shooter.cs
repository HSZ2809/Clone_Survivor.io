using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Mon_Shooter : Monster
    {
        #region Inspector
        [SerializeField] float currentHp;
        [SerializeField] float moveSpeed;

        [SerializeField] Animator anim;

        [Header("Bullet")]
        [SerializeField] MonsterBullet bullet;

        [Header("Sprite Renderer")]
        [SerializeField] SpriteRenderer sr;

        [Header("EXP Type")]
        [SerializeField] EXPShard.Type shardType;
        #endregion

        EXPObjPool EXPPool;
        IEnumerator enumerator;
        readonly WaitForSeconds waitTime = new (10.0f);

        private void Awake()
        {
            chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ChapterCtrl>();
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            EXPPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<EXPObjPool>();
            tag = "Monster";
            gameObject.layer = LayerMask.NameToLayer(tag);
            cc2D = GetComponent<CircleCollider2D>();
        }

        private void Start()
        {
            bullet = Instantiate(bullet);
        }

        private void OnEnable()
        {
            enumerator = ShootBullet();
            currentHp = hp;
            cc2D.enabled = true;
            StartCoroutine(enumerator);
        }

        private void Update()
        {
            if (character.transform.position.x > transform.position.x)
                sr.flipX = false;
            else
                sr.flipX = true;

            if (currentHp > 0)
                transform.position = Vector3.MoveTowards(transform.position, character.transform.position, Time.deltaTime * moveSpeed);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Character"))
            {
                character.Hit(attackPower);
            }
        }

        IEnumerator ShootBullet()
        {
            yield return waitTime;

            Vector3 aim = (transform.position - character.transform.position).normalized;
            float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            bullet.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, angle + 90));
            bullet.gameObject.SetActive(true);
        }

        public override void Hit(float damage)
        {
            currentHp -= damage;

            if (currentHp <= 0)
            {
                cc2D.enabled = false;
                StopCoroutine(enumerator);
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