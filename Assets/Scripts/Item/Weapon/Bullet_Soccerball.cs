using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ZUN
{
    public class Bullet_Soccerball : Bullet
    {
        [SerializeField] private float moveSpeed = 1.0f;
        [SerializeField] private Vector2 direction = Vector2.up;

        private void OnEnable()
        {
            StartCoroutine(DisableBullet());
        }

        private void Update()
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Monster")
            {
                Vector2 normal = other.contacts[0].normal;
                direction = Vector2.Reflect(direction, normal);

                Monster monster = other.gameObject.GetComponent<Monster>();
                monster.Hit(damage);
            }
        }

        // 일정 시간 후 비활성화
        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(5.0f);
            gameObject.SetActive(false);
        }
    }
}