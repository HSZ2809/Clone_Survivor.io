using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace ZUN
{
    public class MonsterBullet : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1.5f;
        [SerializeField] private float disableTime = 5.0f;

        Character character;

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
                gameObject.SetActive(false);
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