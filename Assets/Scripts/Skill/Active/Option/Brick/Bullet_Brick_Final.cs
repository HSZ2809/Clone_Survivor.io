using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Bullet_Brick_Fianl : Bullet
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float disableTime;
        [SerializeField] private float rotationSpeed;

        private void OnEnable()
        {
            StartCoroutine(DisableBullet());
        }

        private void FixedUpdate()
        {
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.up);
            sr[0].transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.TryGetComponent<IDamageable>(out var mon_Damageable))
            {
                mon_Damageable.TryTakeDamage(damage);
            }
        }

        // 일정 시간 후 비활성화
        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(disableTime);
            
            gameObject.SetActive(false);
        }
    }
}