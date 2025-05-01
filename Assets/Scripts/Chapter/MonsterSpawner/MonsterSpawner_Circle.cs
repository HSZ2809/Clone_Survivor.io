using System;
using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class MonsterSpawner_Circle : MonsterSpawner
    {
        #region Inspector
        [Header("Range")]
        [SerializeField] float minDistance = 17f;
        [SerializeField] float maxDistance = 21f;
        #endregion

        IEnumerator enumerator;
        readonly WaitForSeconds waitTime = new (1.0f);

        private void Start()
        {
            enumerator = SummonMonster();
        }

        public override void SetAmount(int _amount)
        {
            StopCoroutine(enumerator);

            amount = _amount;
            
            StartCoroutine(enumerator);
        }

        IEnumerator SummonMonster()
        {
            Vector2 randomVec2;
            float randomAngle;
            float randomDistance;
            float x;
            float y;

            while (true)
            {
                while (amount > currentAmount)
                {
                    randomAngle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
                    randomDistance = UnityEngine.Random.Range(minDistance, maxDistance);

                    x = Mathf.Cos(randomAngle) * randomDistance;
                    y = Mathf.Sin(randomAngle) * randomDistance;

                    randomVec2.x = transform.position.x + x;
                    randomVec2.y = transform.position.y + y;

                    objPool.TryDequeue(out NomalMonster _monster);

                    if (_monster == null)
                    {
                        _monster = Instantiate(monster);
                        _monster.SetMonsterSpec(hp, ap);
                        _monster.SetSpawner(this);
                    }

                    _monster.transform.position = randomVec2;
                    _monster.gameObject.SetActive(true);
                    currentAmount += 1;
                }

                yield return waitTime;
            }
        }

        public void SetSpawnDistance(float expand)
        {
            minDistance += expand;
            maxDistance += expand;
        }
    }
}