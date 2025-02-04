using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Mon_Shooter : Monster
    {
        [SerializeField] Animator anim;
        EXPObjPool EXPPool;

        [Header("Bullet")]
        [SerializeField] MonsterBullet bullet;

        [Header("Sprite Renderer")]
        [SerializeField] SpriteRenderer sr;

        [Header("EXP Type")]
        [SerializeField] EXPShard.Type shardType;

        IEnumerator enumerator;
        readonly WaitForSeconds waitTime = new (10.0f);

        private void Awake()
        {
            tag = "Monster";
            gameObject.layer = LayerMask.NameToLayer(tag);
            cc2D = GetComponent<CircleCollider2D>();
        }

        private void Start()
        {
            EXPPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<EXPObjPool>();
            bullet = Instantiate(bullet);
        }

        private void OnEnable()
        {
            enumerator = ShootBullet();
            hp = maxHp;
            cc2D.enabled = true;
            StartCoroutine(enumerator);
        }

        private void Update()
        {
            if (characterTF.position.x > transform.position.x)
                sr.flipX = false;
            else
                sr.flipX = true;

            if (hp > 0)
                transform.position = Vector3.MoveTowards(transform.position, characterTF.position, Time.deltaTime * moveSpeed);
        }

        IEnumerator ShootBullet()
        {
            yield return waitTime;

            Vector3 aim = (transform.position - characterTF.position).normalized;
            float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            bullet.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, angle + 90));
            bullet.gameObject.SetActive(true);
        }

        public override void Hit(float damage)
        {
            hp -= damage;

            if (hp <= 0)
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