using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Bullet_Soccerball : Bullet
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float disableTime;
        [SerializeField] private Vector2 direction = Vector2.up;

        public float MoveSpeed { set { moveSpeed = value; } }

        private Camera mainCam;

        private void Start()
        {
            mainCam = Camera.main;
        }

        private void OnEnable()
        {
            direction = Random.insideUnitCircle.normalized;
            StartCoroutine(DisableBullet());
        }

        private void Update()
        {
            CamBoundCheck();
            transform.Translate(moveSpeed * Time.deltaTime * direction);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Monster"))
            {
                Vector2 normal = other.contacts[0].normal;
                direction = Vector2.Reflect(direction, normal);

                Monster monster = other.gameObject.GetComponent<Monster>();
                monster.Hit(damage);
            }
        }

        private void CamBoundCheck()
        {
            Vector3 pos = transform.position;
            Vector3 camPos = mainCam.transform.position;

            float halfHeight = mainCam.orthographicSize;
            float halfWidth = halfHeight * mainCam.aspect;

            if (pos.x > camPos.x + halfWidth || pos.x < camPos.x - halfWidth)
                direction.x = -direction.x;

            if (pos.y > camPos.y + halfHeight || pos.y < camPos.y - halfHeight)
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