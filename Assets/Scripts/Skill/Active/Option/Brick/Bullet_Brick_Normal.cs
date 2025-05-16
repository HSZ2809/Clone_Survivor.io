using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class Bullet_Brick_Normal : Bullet
    {
        [SerializeField] private float yForce;
        [SerializeField] private float xForceRange;
        [SerializeField] private float disableTime;
        [SerializeField] private int maxDurability;
        int durability;

        [SerializeField] Rigidbody2D rb;
        IObjectPool<Bullet_Brick_Normal> objPool;

        bool isReleased = false;

        private void OnEnable()
        {
            isReleased = false;
            durability = maxDurability;
            float angle = Random.Range(-xForceRange, xForceRange);
            rb.AddForce(new Vector2(angle, yForce), ForceMode2D.Impulse);
            StartCoroutine(DisableBullet());
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.TryGetComponent<IDamageable>(out var mon_Damageable))
            {
                mon_Damageable.TakeDamage(damage);
                durability -= 1;

                if(durability < 1 && !isReleased)
                {
                    StopCoroutine(DisableBullet());
                    if (objPool != null)
                        objPool.Release(this);
                    else
                        Destroy(gameObject);
                }
            }
        }

        // 일정 시간 후 비활성화
        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(disableTime);
            if (!isReleased)
            {
                isReleased = true;
                objPool.Release(this);
            }
        }

        public void SetBulletPool(IObjectPool<Bullet_Brick_Normal> pool)
        {
            objPool = pool;
        }
    }
}