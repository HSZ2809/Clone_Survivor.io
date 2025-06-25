using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Boss_Bouncebloom_Bullet : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1.5f;
        [SerializeField] private float disableTime = 5.0f;

        Character character;
        public Vector2 Direction { get; set; }

        public float Damage { get; set; }

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            tag = "MonsterBullet";
            gameObject.layer = LayerMask.NameToLayer(tag);
        }

        private void Start()
        {
            StartCoroutine(DisableBullet());
        }

        private void OnEnable()
        {
            Direction = (character.transform.position - transform.position).normalized;
        }

        private void FixedUpdate()
        {
            transform.Translate(moveSpeed * Time.deltaTime * Direction);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Fence"))
            {
                Vector2 collisionPoint = collision.ClosestPoint(transform.position);
                Vector2 collisionNormal = (transform.position - (Vector3)collisionPoint).normalized;
                Direction = Vector2.Reflect(Direction, collisionNormal);
                return;
            }

            GameObject parentObject = collision.gameObject.transform.parent?.gameObject;

            if (parentObject != null && parentObject.CompareTag("Character"))
            {
                character.TakeDamage(Damage);
                return;
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