using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Bullet_Brick : Bullet
    {
        [SerializeField] private float yForce = 1.0f;
        [SerializeField] private float xForceRange = 1.0f;
        [SerializeField] private float disableTime = 1.0f;

        [SerializeField] Rigidbody2D rb;

        private void OnEnable()
        {
            float angle = Random.Range(-xForceRange, xForceRange);
            rb.AddForce(new Vector2(angle, yForce), ForceMode2D.Impulse);
            StartCoroutine(DisableBullet());
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Monster"))
            {
                Monster monster = other.gameObject.GetComponent<Monster>();
                monster.Hit(damage);
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