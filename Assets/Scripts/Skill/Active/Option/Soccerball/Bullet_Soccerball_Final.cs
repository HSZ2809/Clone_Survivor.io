using UnityEngine;

namespace ZUN
{
    public class Bullet_Soccerball_Final : Bullet
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip clip;

        [SerializeField] float moveSpeed;
        [SerializeField] float disableTime;
        [SerializeField] Vector2 direction;

        public float MoveSpeed { set { moveSpeed = value; } }
        public float CharMoveSpeed { get; set; }

        Camera mainCam;
        Soccerball_Final soccerball_Final;

        private void Start()
        {
            mainCam = Camera.main;
        }

        private void OnEnable()
        {
            direction = Random.insideUnitCircle.normalized;
        }

        private void FixedUpdate()
        {
            if (soccerball_Final.IsDurationEnd)
                soccerball_Final.ObjPool.Release(this);

            CamBoundCheck();
            transform.Translate(moveSpeed * Time.deltaTime * direction);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Monster"))
            {
                Vector2 normal = other.contacts[0].normal;
                direction = Vector2.Reflect(direction, normal);

                if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
                    damageable.TryTakeDamage(damage);

                soccerball_Final.ShootBullet(transform);
                soccerball_Final.ShootBullet(transform);

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

        public void SetSkill(Soccerball_Final soccerball_Final)
        {
            this.soccerball_Final = soccerball_Final;
        }
    }
}