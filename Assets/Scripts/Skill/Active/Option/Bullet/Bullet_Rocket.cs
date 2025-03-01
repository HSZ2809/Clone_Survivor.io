using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class Bullet_Rocket : Bullet
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float disableTime;
        // [SerializeField] CapsuleCollider2D collider;

        IObjectPool<Bullet_Rocket> objPool;

        private void OnEnable()
        {
            StartCoroutine(DisableBullet());
        }

        private void Update()
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.CompareTag("Monster"))
            {
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
            // 폭발 로직 + 애니메이션 실행
            objPool.Release(this);
        }

        public void SetBulletPool(IObjectPool<Bullet_Rocket> pool)
        {
            objPool = pool;
        }
    }
}
