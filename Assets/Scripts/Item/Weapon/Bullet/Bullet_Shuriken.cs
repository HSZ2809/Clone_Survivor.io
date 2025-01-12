using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Bullet_Shuriken : Bullet
    {
        [SerializeField] private float moveSpeed = 1.0f;
        [SerializeField] private float disableTime = 1.0f;

        private void OnEnable()
        {
            StartCoroutine(DisableBullet());
        }

        private void Update()
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Monster")
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