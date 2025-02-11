using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Bullet_Soccerball : Bullet
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip clip;

        [SerializeField] private float moveSpeed;
        [SerializeField] private float disableTime;
        [SerializeField] private Vector2 direction;

        public float MoveSpeed { set { moveSpeed = value; } }
        public float CharMoveSpeed { get; set; }

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

                other.gameObject.GetComponent<IMon_Damageable>().TakeDamage(damage);

                audioSource.PlayOneShot(clip);
            }
        }

        private void CamBoundCheck()
        {
            Vector3 pos = transform.position;
            Vector3 camPos = mainCam.transform.position;

            float halfHeight = mainCam.orthographicSize;
            float halfWidth = halfHeight * mainCam.aspect;

            if (pos.x > camPos.x + halfWidth)
            {
                direction.x = -direction.x;
                transform.Translate(CharMoveSpeed * Time.deltaTime * Vector3.left);
            }   
            if (pos.x < camPos.x - halfWidth)
            {
                direction.x = -direction.x;
                transform.Translate(CharMoveSpeed * Time.deltaTime * Vector3.right);
            }
            if (pos.y > camPos.y + halfHeight)
            {
                direction.y = -direction.y;
                transform.Translate(CharMoveSpeed * Time.deltaTime * Vector3.down);
            }   
            if (pos.y < camPos.y - halfHeight)
            {
                direction.y = -direction.y;
                transform.Translate(CharMoveSpeed * Time.deltaTime * Vector3.up);
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