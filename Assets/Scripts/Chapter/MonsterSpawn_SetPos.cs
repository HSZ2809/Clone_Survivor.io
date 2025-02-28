using System;
using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class MonsterSpawn_SetPos : MonoBehaviour
    {
        Character character;

        [Header("Monster")]
        [SerializeField] Monster monster;
        [SerializeField] float hp;
        [SerializeField] float attackPower;
        [SerializeField] Monster[] objPool;

        [Header("Amount of monsters")]
        [SerializeField] int amount;
        [SerializeField] int swarmSize;

        [Header("Range")]
        [SerializeField] float distance;

        IEnumerator enumerator;
        readonly WaitForSeconds waitTime = new(5.0f);

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

                for (int i = before; i < after; i++)
                {
                    Monster mon = Instantiate(monster, transform.position, transform.rotation);
                    mon.SetMonsterSpec(hp, attackPower);
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