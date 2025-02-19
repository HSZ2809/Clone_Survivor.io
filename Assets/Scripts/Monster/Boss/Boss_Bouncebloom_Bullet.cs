using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Boss_Bouncebloom_Bullet : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1.5f;
        [SerializeField] private float disableTime = 5.0f;
        
        private Vector2 direction;

        public float Damage { get; set; }

        private void Awake()
        {
            tag = "MonsterBullet";
            gameObject.layer = LayerMask.NameToLayer(tag);
        }

        private void Start()
        {
            StartCoroutine(DisableBullet());
        }

        private void FixedUpdate()
        {
            transform.Translate(moveSpeed * Time.deltaTime * direction);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Character"))
            {
                collision.gameObject.GetComponent<Character>().TakeDamage(Damage);
            }

            if (collision.gameObject.CompareTag("Fence"))
            {
                //Vector2 collisionPoint = collision.ClosestPoint(transform.position);
                //Vector2 collisionNormal = (transform.position - (Vector3)collisionPoint).normalized;
                //direction = Vector2.Reflect(direction, collisionNormal);
            }
        }

        // 일정 시간 후 비활성화
        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(disableTime);
            Destroy(gameObject);
        }
    }
}