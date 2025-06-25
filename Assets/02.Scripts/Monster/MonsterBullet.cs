using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class MonsterBullet : MonoBehaviour, IBlockable
    {
        [SerializeField] private float moveSpeed = 1.5f;
        [SerializeField] private float disableTime = 5.0f;

        Character character;
        IObjectPool<MonsterBullet> bulletPool;

        public float Damage { get; set; }

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            tag = "MonsterBullet";
            gameObject.layer = LayerMask.NameToLayer(tag);
        }

        private void OnEnable()
        {
            StartCoroutine(DisableBullet());
        }

        private void Update()
        {
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.up);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject parentObject = collision.gameObject.transform.parent?.gameObject;

            if (parentObject != null && parentObject.CompareTag("Character"))
            {
                character.TakeDamage(Damage);
                ReleaseBullet();
            }
        }

        // 일정 시간 후 비활성화
        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(disableTime);
            ReleaseBullet();
        }

        public void SetBulletPool(IObjectPool<MonsterBullet> pool)
        {
            bulletPool = pool;
        }

        public void ReleaseBullet()
        {
            bulletPool.Release(this);
        }

        public void Block()
        {
            bulletPool.Release(this);
        }
    }
}