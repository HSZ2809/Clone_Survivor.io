using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Magnet : MonoBehaviour, IGetDropedItem
    {
        [SerializeField] private Collider2D col;
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

            // 자석 취득 시 사운드 + 시각 효과 추가 필요
            GameObject[] dropItems = GameObject.FindGameObjectsWithTag("DropedItem");
            foreach (GameObject dropItem in dropItems)
                if (TryGetComponent<EXPShard>(out var shard))
                    shard.GetDropedItem(character);
            
            Destroy(this);
        }
    }
}
