using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace ZUN
{
    public class Bullet_Shield : Bullet
    {
        [SerializeField] private Transform sprite;
        [SerializeField] private SpriteRenderer[] Runes;
        [SerializeField] private SpriteRenderer glow;

        [SerializeField] private float attackTerm = 1.0f;
        [SerializeField] private float range = 1.0f;
        LayerMask monsterLayer;

        private void Start()
        {
            monsterLayer = (1 << LayerMask.NameToLayer("Monster"));
            StartCoroutine(Attack());
        }

        IEnumerator Attack()
        {
            while (true)
            {
                yield return new WaitForSeconds(attackTerm);

                Collider2D[] monsterCol;
                monsterCol = Physics2D.OverlapCircleAll(transform.position, range, monsterLayer);

                foreach(Collider2D col in monsterCol)
                {
                    col.gameObject.GetComponent<IMon_Damageable>().TakeDamage(damage);
                }
            }
        }

        public void SetRange(float r)
        {
            range = r;
            transform.localScale = new Vector3(range, range, 0);
        }
    }
}