using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Bullet_Shield : Bullet
    {
        [SerializeField] private SpriteRenderer glow;

        [SerializeField] private float attackTerm = 1.0f;
        [SerializeField] private float range = 1.0f;

        private HashSet<IDamageable> monstersInArea = new HashSet<IDamageable>();
        private Dictionary<IDamageable, Coroutine> activeCoroutines = new Dictionary<IDamageable, Coroutine>();

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.CompareTag("Monster"))
            {
                if (coll.gameObject.TryGetComponent<IDamageable>(out var monster) && !monstersInArea.Contains(monster))
                {
                    monstersInArea.Add(monster);

                    Coroutine damageCoroutine = StartCoroutine(Attack(monster));
                    activeCoroutines[monster] = damageCoroutine;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D coll)
        {
            if (coll.gameObject.CompareTag("Monster"))
            {
                if (coll.gameObject.TryGetComponent<IDamageable>(out var monster) && monstersInArea.Contains(monster))
                {
                    monstersInArea.Remove(monster);

                    if (activeCoroutines.TryGetValue(monster, out Coroutine coroutine))
                    {
                        activeCoroutines.Remove(monster);

                        if (coroutine != null)
                        {
                            StopCoroutine(coroutine);
                        }
                    }
                }
            }
        }

        IEnumerator Attack(IDamageable monster)
        {
            while (true)
            {
                if (monster == null)
                {
                    if (activeCoroutines.ContainsKey(monster))
                    {
                        activeCoroutines.Remove(monster);
                    }
                    yield break;
                }

                if (monster.TakeDamage(damage) <= 0)
                {
                    if (activeCoroutines.ContainsKey(monster))
                    {
                        activeCoroutines.Remove(monster);
                    }
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