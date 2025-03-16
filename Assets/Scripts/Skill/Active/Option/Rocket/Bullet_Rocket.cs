using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class Bullet_Rocket : Bullet
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float disableTime;
        [SerializeField] private float explodeRange;
        private bool isExplode;

        IObjectPool<Bullet_Rocket> objPool;
        LayerMask monsterLayer;
        Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
            monsterLayer = (1 << LayerMask.NameToLayer("Monster"));
        }

        private void OnEnable()
        {
            isExplode = false;
            StartCoroutine(DisableBullet());
        }

        private void Update()
        {
            if (!isExplode)
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.CompareTag("Monster"))
            {
                StopCoroutine(DisableBullet());
                isExplode = true;
                coll.gameObject.GetComponent<IMon_Damageable>().TakeDamage(damage);
                Explode();
            }
        }

        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(disableTime);
            objPool.Release(this);
        }

        void Explode()
        {
            Collider2D[] monsterCol = Physics2D.OverlapCircleAll(transform.position, explodeRange, monsterLayer);
            foreach (var coll in monsterCol)
            {
                if (coll.gameObject.TryGetComponent<IMon_Damageable>(out var mon_Damageable))
                {
                    mon_Damageable.TakeDamage(damage);
                }
            }
            anim.SetTrigger("Explode");
        }

        void ReleaseBullet()
        {
            objPool.Release(this);
        }

        public void SetBulletPool(IObjectPool<Bullet_Rocket> pool)
        {
            objPool = pool;
        }
    }
}
