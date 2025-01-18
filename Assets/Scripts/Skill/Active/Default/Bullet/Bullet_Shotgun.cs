using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Bullet_Shotgun : Bullet
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float disableTime;

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
                Monster monster = coll.gameObject.GetComponent<Monster>();
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