using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class Bullet_Shotgun : Bullet
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float disableTime;

        IObjectPool<Bullet_Shotgun> objPool;

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
                objPool.Release(this);
            }
        }

        // 일정 시간 후 비활성화
        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(disableTime);
            objPool.Release(this);
        }

        public void SetBulletPool(IObjectPool<Bullet_Shotgun> pool)
        {
            objPool = pool;
        }
    }
}