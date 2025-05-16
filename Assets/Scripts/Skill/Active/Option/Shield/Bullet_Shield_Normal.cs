using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Bullet_Shield_Normal : Bullet
    {
        [SerializeField] float attackTerm;
        [SerializeField] float range;

        readonly HashSet<GameObject> monstersInArea = new();
        readonly Dictionary<GameObject, Coroutine> activeCoroutines = new();

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.TryGetComponent<IDamageable>(out var damageable) && !monstersInArea.Contains(coll.gameObject))
            {
                monstersInArea.Add(coll.gameObject);

                Coroutine damageCoroutine = StartCoroutine(Attack(coll.gameObject, damageable));
                activeCoroutines[coll.gameObject] = damageCoroutine;
            }
        }

        private void OnTriggerExit2D(Collider2D coll)
        {
            if (monstersInArea.Contains(coll.gameObject))
            {
                monstersInArea.Remove(coll.gameObject);

                if (activeCoroutines.TryGetValue(coll.gameObject, out Coroutine coroutine))
                {
                    activeCoroutines.Remove(coll.gameObject);

                    if (coroutine != null)
                    {
                        StopCoroutine(coroutine);
                    }
                }
            }
        }

        IEnumerator Attack(GameObject monster, IDamageable damageable)
        {
            while (true)
            {
                if (monster == null)
                {
                    if (activeCoroutines.ContainsKey(monster))
                        activeCoroutines.Remove(monster);

                    yield break;
                }

                if (damageable.TakeDamage(damage) <= 0)
                {
                    if (activeCoroutines.ContainsKey(monster))
                        activeCoroutines.Remove(monster);

                    yield break;
                }

                yield return new WaitForSeconds(attackTerm);
            }
        }

        public void SetRange(float r)
        {
            range = r;
            transform.localScale = new Vector3(range, range, 0);
        }
    }
}