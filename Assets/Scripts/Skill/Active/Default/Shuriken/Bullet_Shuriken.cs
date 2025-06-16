using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class Bullet_Shuriken : Bullet
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float disableTime;

        IObjectPool<Bullet_Shuriken> objPool;

        bool isReleased = false;

        private void OnEnable()
        {
            isReleased = false;
            StartCoroutine(DisableBullet());
        }

        private void FixedUpdate()
        {
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.up);
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.TryGetComponent<IDamageable>(out var mon_Damageable))
            {
                if (!isReleased)
                {
                    isReleased = true;
                    StopCoroutine(DisableBullet());
                    mon_Damageable.TryTakeDamage(damage);
                    if (objPool != null)
                        objPool.Release(this);
                    else
                        Destroy(gameObject);
                }
            }
        }

        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(disableTime);
            if (!isReleased)
            {
                isReleased = true;
                if (objPool != null)
                    objPool.Release(this);
                else
                    Destroy(gameObject);
            }
        }

        public void SetBulletPool(IObjectPool<Bullet_Shuriken> pool)
        {
            objPool = pool;
        }
    }
}