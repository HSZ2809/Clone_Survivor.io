using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Bullet_Soccerball : Bullet
    {
        [SerializeField] private float moveSpeed = 1.0f;
        [SerializeField] private float disableTime = 1.0f;
        [SerializeField] private Vector2 direction = Vector2.up;

        private Camera mainCam;

        private void Start()
        {
            mainCam = Camera.main;
        }

        private void OnEnable()
        {
            direction = Random.insideUnitCircle;
            StartCoroutine(DisableBullet());
        }

        private void Update()
        {
            CamBoundCheck();
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

        private void CamBoundCheck()
        {
            Vector3 position = transform.position;
            float halfHeight = mainCam.orthographicSize;
            float halfWidth = halfHeight * mainCam.aspect;

            if(position.x > halfWidth || position.x < -halfWidth)
                direction.x = -direction.x;

            if(position.y > halfHeight || position.y < -halfHeight)
                direction.y = -direction.y;
        }

        // 일정 시간 후 비활성화
        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(disableTime);
            gameObject.SetActive(false);
        }
    }
}