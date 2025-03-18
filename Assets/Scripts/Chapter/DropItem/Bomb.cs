using System.Collections;
using UnityEngine;

namespace ZUN
{    
    public class Bomb : MonoBehaviour, IGetDropedItem
    {
        [SerializeField] private Collider2D col;
        [SerializeField] private float attackPoint;
        [SerializeField] private float initialSpeed;
        float gravity = 50f;

        public void GetDropedItem(Character character)
        {
            StartCoroutine(MoveTowardCharacter(character));
        }

        IEnumerator MoveTowardCharacter(Character character)
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

            while (Vector3.Distance(transform.position, target.position) > 0.3f)
            {
                speed += gravity * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                yield return null;
            }

            transform.position = target.position;

            // 화면 전체가 0.3f 에 걸쳐서 하얗게 되었다가 다시 돌아오는 로직 필요
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
            foreach (GameObject monster in monsters)
                if (TryGetComponent<IMon_Damageable>(out var damageable))
                    damageable.TakeDamage(attackPoint);
            
            Destroy(this);
        }
    }
}
