using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class EXPShard : MonoBehaviour
    {
        IObjectPool<EXPShard> pool;

        public enum Type
        {
            SMALL = 0,
            GREEN = 1,
            BLUE = 2,
            YELLOW = 3
        }

        [SerializeField] private Type shardType;
        [SerializeField] private Collider2D col;
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private int exp;
        [SerializeField] private float initialSpeed;
        float gravity = 50f;

        [SerializeField] private Sprite[] sprites;
        [SerializeField] private int[] amount;

        private void OnEnable()
        {
            col.enabled = true;
        }

        public Type ShardType { get { return shardType; } }

        public void SetType(Type type)
        {
            shardType = type;
            sr.sprite = sprites[(int)type];
            exp = amount[(int)type];
        }

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.CompareTag("Character"))
        //    {
        //        character = collision.GetComponent<Character>();
        //        StartCoroutine(DraggedToChar(collision.transform));
        //    }
        //}

        public void SetPool(IObjectPool<EXPShard> pool)
        {
            this.pool = pool;
        }

        public void GetEXP(Character character)
        {
            StartCoroutine(DraggedToChar(character));
        }

        IEnumerator DraggedToChar(Character character)
        {
            col.enabled = false;
            float speed = initialSpeed;

            Transform target = character.transform;
            Vector3 direction = (transform.position - target.position).normalized;

            while(speed > 0f)
            {
                transform.position += speed * Time.deltaTime * direction;
                speed -= gravity * Time.deltaTime;
                yield return null;
            }

            while (Vector3.Distance(transform.position, target.position) > 0.1f)
            {
                speed += gravity * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                yield return null;
            }

            transform.position = target.position;
            character.AddExp(exp);
            pool.Release(this);
        }
    }
}