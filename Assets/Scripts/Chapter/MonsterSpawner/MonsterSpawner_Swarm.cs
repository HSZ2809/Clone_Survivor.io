using System;
using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class MonsterSpawner_Swarm : MonsterSpawner
    {
        [Header("Spawn")]
        [SerializeField] Transform[] spawnTransform;
        [SerializeField] int swarmSize;

        IEnumerator enumerator;
        readonly WaitForSeconds waitTime = new(5.0f);

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
            int randomNum;
            bool transformCheck = true;

            if (spawnTransform == null)
            {
                Debug.LogWarning("spawnTransform missing");
                transformCheck = false;
            }    

            while (transformCheck)
            {
                while (amount - swarmSize > currentAmount)
                {
                    randomNum = UnityEngine.Random.Range(0, spawnTransform.Length);

                    for (int i = 0; i < swarmSize; i++)
                    {
                        objPool.TryDequeue(out NomalMonster _monster);

                        if (_monster == null)
                        {
                            _monster = Instantiate(monster);
                            _monster.SetMonsterSpec(hp, ap);
                            _monster.SetSpawner(this);    
                        }

                        _monster.transform.position = spawnTransform[randomNum].position;
                        _monster.gameObject.SetActive(true);
                        currentAmount += 1;
                    }
                }

                yield return waitTime;
            }
        }
    }
}