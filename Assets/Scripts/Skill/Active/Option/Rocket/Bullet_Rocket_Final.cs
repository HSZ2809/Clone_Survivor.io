using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class Bullet_Rocket_Final : Bullet
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float disableTime;
        [SerializeField] private float explodeRange;
        private bool isExplode;
        [SerializeField] private int maxDurability;
        int durability;

        IObjectPool<Bullet_Rocket_Final> objPool;
        LayerMask monsterLayer;
        Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
            monsterLayer = (1 << LayerMask.NameToLayer("Target"));
        }

        private void OnEnable()
        {
            isExplode = false;
            durability = maxDurability;
            StartCoroutine(DisableBullet());
        }

        private void Update()
        {
            if (!isExplode)
                transform.Translate(moveSpeed * Time.deltaTime * Vector3.up);
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.TryGetComponent<IDamageable>(out var mon_Damageable))
            {
                mon_Damageable.TakeDamage(damage);
                durability -= 1;

                if (durability < 1)
                {
                    StopCoroutine(DisableBullet());
                    isExplode = true;
                    Explode();
                }
            }
        }

        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(disableTime);

            isExplode = true;
            anim.Play("Bullet_Rocket_Final_Explode");
        }

        void Explode()
        {
            Collider2D[] monsterCol = Physics2D.OverlapCircleAll(transform.position, explodeRange, monsterLayer);
            foreach (var coll in monsterCol)
            {
                if (coll.gameObject.TryGetComponent<IDamageable>(out var mon_Damageable))
                {
                    mon_Damageable.TakeDamage(damage);
                }
            }
            anim.Play("Bullet_Rocket_Final_Explode");
        }

        void ReleaseBullet()
        {
            objPool.Release(this);
        }

        public void SetBulletPool(IObjectPool<Bullet_Rocket_Final> pool)
        {
            objPool = pool;
        }
    }
}
