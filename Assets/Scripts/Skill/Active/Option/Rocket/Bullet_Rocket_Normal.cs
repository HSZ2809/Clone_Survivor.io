using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class Bullet_Rocket_Normal : Bullet
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float disableTime;
        [SerializeField] private float explodeRange;
        private bool isExplode;

        IObjectPool<Bullet_Rocket_Normal> objPool;
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
            StartCoroutine(DisableBullet());
        }

        private void Update()
        {
            if (!isExplode)
                transform.Translate(moveSpeed * Time.deltaTime * Vector3.up);
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.CompareTag("Monster"))
            {
                StopCoroutine(DisableBullet());
                isExplode = true;
                coll.gameObject.GetComponent<IDamageable>().TryTakeDamage(damage);
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
                if (coll.gameObject.TryGetComponent<IDamageable>(out var mon_Damageable))
                {
                    mon_Damageable.TryTakeDamage(damage);
                }
            }
            anim.SetTrigger("Explode");
        }

        void ReleaseBullet()
        {
            if (objPool != null)
                objPool.Release(this);
            else
                Destroy(gameObject);
        }

        public void SetBulletPool(IObjectPool<Bullet_Rocket_Normal> pool)
        {
            objPool = pool;
        }
    }
}
