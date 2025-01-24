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
            EXPPool = GameObject.FindGameObjectWithTag("StageCtrl").GetComponent<EXPObjPool>();
            speed = moveSpeed;
        }

        private void Update()
        {
            //Vector3 moveDirection = (character.position - transform.position).normalized;
            //moveDirection *= moveSpeed;

            //transform.Translate(moveDirection * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, character.position, Time.deltaTime * speed);
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
            gameObject.SetActive(false);
        }
    }
}