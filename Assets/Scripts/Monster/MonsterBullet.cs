using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class MonsterBullet : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float moveSpeed = 1.0f;
        [SerializeField] private float disableTime = 5.0f;

        public float Damage { get { return damage; } }

        private void Awake()
        {
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

        // 일정 시간 후 비활성화
        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(disableTime);
            gameObject.SetActive(false);
        }

        public void SetFalse()
        {
            gameObject.SetActive(false);
        }
    }
}