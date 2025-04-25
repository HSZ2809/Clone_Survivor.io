using System;
using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class MonsterSpawner_Swarm : MonsterSpawner
    {
        [SerializeField] int swarmSize;

        [Header("Range")]
        [SerializeField] float distance;

        [SerializeField] Monster[] objPool = new Monster[1];
        IEnumerator enumerator;
        readonly WaitForSeconds waitTime = new(5.0f);

        private void Start()
        {
            enumerator = SummonMonster();
        }

        public override void SetAmount(int _amount)
        {
            StopCoroutine(enumerator);

            if (objPool.Length < _amount)
            {
                int before = objPool.Length;
                int after = _amount;

                Array.Resize(ref objPool, after);

                for (int i = before; i < after; i++)
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
            // Transform moveDir;
            Vector2 randomVec2 = new(0, 0);
            int randomNum;

            while (true)
            {
                // moveDir = character.GetMoveDir();
                randomNum = UnityEngine.Random.Range(0, 3);

                switch (randomNum)
                {
                    case 0:
                        randomVec2.x = 1;
                        randomVec2.y = 1;
                        break;
                    case 1:
                        randomVec2.x = 1;
                        randomVec2.y = -1;
                        break;
                    case 2:
                        randomVec2.x = -1;
                        randomVec2.y = 1;
                        break;
                    case 3:
                        randomVec2.x = -1;
                        randomVec2.y = -1;
                        break;
                }

                randomVec2 = randomVec2.normalized;
                randomVec2.x = character.transform.position.x + randomVec2.x * distance;
                randomVec2.y = character.transform.position.y + randomVec2.y * distance;

                for (int i = 0; i < amount; i++)
                {
                    if (!objPool[i].gameObject.activeSelf)
                    {
                        objPool[i].gameObject.transform.position = randomVec2;
                        objPool[i].gameObject.SetActive(true);
                        yield return null;
                    }
                }

                yield return waitTime;
            }
        }
    }
}