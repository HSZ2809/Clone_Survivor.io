using System;
using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class MonsterSpawn : MonoBehaviour
    {
        Character character;

        [Header("Monster")]
        [SerializeField] Monster monster;
        [SerializeField] float hp;
        [SerializeField] float ap;
        [SerializeField] Monster[] objPool;

        [Header("Amount of monsters")]
        [SerializeField] int amount;

        [Header("Range")]
        [SerializeField] float minDistance = 17f;
        [SerializeField] float maxDistance = 21f;

        IEnumerator enumerator;
        readonly WaitForSeconds waitTime = new (1.0f);

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        }

        private void Start()
        {
            enumerator = SummonMonster();
        }

        public void SetAmount(int _amount)
        {
            StopCoroutine(enumerator);

            if (objPool.Length < _amount)
            {
                int before = objPool.Length;
                int after = _amount;

                Array.Resize(ref objPool, after);

                for(int i = before; i < after; i++)
                {
                    Monster mon = Instantiate(monster, transform.position, transform.rotation);
                    mon.SetMonsterSpec(hp, ap);
                    objPool[i] = mon;
                }
            }

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
                for (int i = 0; i < amount; i++)
                {
                    if (!objPool[i].gameObject.activeSelf)
                    {
                        randomAngle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
                        randomDistance = UnityEngine.Random.Range(minDistance, maxDistance);

                        x = Mathf.Cos(randomAngle) * randomDistance;
                        y = Mathf.Sin(randomAngle) * randomDistance;

                        randomVec2.x = character.transform.position.x + x;
                        randomVec2.y = character.transform.position.y + y;

                        objPool[i].gameObject.transform.position = randomVec2;
                        objPool[i].gameObject.SetActive(true);
                    }
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