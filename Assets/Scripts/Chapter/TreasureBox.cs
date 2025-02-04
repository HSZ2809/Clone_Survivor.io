using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class TreasureBox : MonoBehaviour
    {
        [SerializeField] private Collider2D col;
        [SerializeField] private float initialSpeed;
        float gravity = 50f;

        public void GetTreasureBox(Character character)
        {
            StartCoroutine(DraggedToChar(character));
        }

        IEnumerator DraggedToChar(Character character)
        {
            col.enabled = false;
            float speed = initialSpeed;

            Transform target = character.transform;
            Vector3 direction = (transform.position - target.position).normalized;

            while (speed > 0f)
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
            character.GetTreasureBox();
            Destroy(gameObject);
        }
    }
}
