using UnityEngine;

namespace ZUN
{
    public class Mon_Chaser : Monster
    {
        [SerializeField] Animator anim;
        EXPObjPool EXPPool;

        [Header("Sprite Renderer")]
        [SerializeField] private SpriteRenderer sr;

        [Header("EXP Type")]
        [SerializeField] private EXPShard.Type shardType;

        private void Awake()
        {
            tag = "Monster";
            gameObject.layer = LayerMask.NameToLayer(tag);
            cc2D = GetComponent<CircleCollider2D>();
        }

        private void OnEnable()
        {
            hp = maxHp;
            cc2D.enabled = true;
        }

        private void Start()
        {
            EXPPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<EXPObjPool>();
        }

        private void Update()
        {
            if(characterTF.position.x > transform.position.x)
                sr.flipX = false;
            else
                sr.flipX = true;

            if(hp > 0)
                transform.position = Vector3.MoveTowards(transform.position, characterTF.position, Time.deltaTime * moveSpeed);
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