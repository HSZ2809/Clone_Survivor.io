using UnityEngine;

namespace ZUN
{
    public class Zombie : Monster
    {
        [SerializeField] Animator anim;
        EXPObjPool EXPPool;

        [Header("EXP Type")]
        [SerializeField] private EXPShard.Type shardType;

        private void OnEnable()
        {
            speed = moveSpeed;
        }

        private void Start()
        {
            EXPPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<EXPObjPool>();
            speed = moveSpeed;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, characterTF.position, Time.deltaTime * speed);
        }

        public override void Hit(float damage)
        {
            hp -= damage;

            if (hp < 0)
            {
                speed = 0;
                anim.SetTrigger("Die");
            }
        }

        public void Die()
        {
            EXPPool.SetShard(shardType, transform.position);
            chapterCtrl.AddKillCount();
            gameObject.SetActive(false);
        }
    }
}