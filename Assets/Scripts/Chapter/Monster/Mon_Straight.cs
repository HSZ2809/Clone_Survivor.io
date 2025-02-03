using UnityEngine;

namespace ZUN
{
    public class Mon_Straight : Monster
    {
        [SerializeField] Animator anim;
        EXPObjPool EXPPool;

        [Header("Sprite Renderer")]
        [SerializeField] private SpriteRenderer sr;

        [Header("EXP Type")]
        [SerializeField] private EXPShard.Type shardType;

        Vector2 moveDirection;

        private void OnEnable()
        {
            hp = maxHp;
            cc2D.enabled = true;
            moveDirection = (characterTF.position - transform.position).normalized;

            if (characterTF.position.x > transform.position.x)
                sr.flipX = false;
            else
                sr.flipX = true;
        }

        private void Start()
        {
            EXPPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<EXPObjPool>();
        }

        private void Update()
        {
            if (hp > 0)
                transform.Translate(moveSpeed * Time.deltaTime * moveDirection);
        }

        public override void Hit(float damage)
        {
            hp -= damage;

            if (hp <= 0)
            {
                cc2D.enabled = false;
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