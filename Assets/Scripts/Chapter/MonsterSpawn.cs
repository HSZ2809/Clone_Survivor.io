using System;
using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class MonsterSpawn : MonoBehaviour
    {
        ChapterCtrl chapterCtrl;
        Character character;

        [Header("Monster")]
        [SerializeField] Monster monster;
        [SerializeField] Monster[] objPool;

        [Header("Amount of monsters")]
        [SerializeField] int amount;

        [Header("Range")]
        [SerializeField] float minDistance = 16f;
        [SerializeField] float maxDistance = 18f;
        
        IEnumerator enumerator;
        readonly WaitForSeconds waitTime = new (1.0f);

        private void Awake()
        {
            chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ChapterCtrl>();
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
                    mon._chapterCtrl = chapterCtrl;
                    mon.CharacterTF = character.gameObject.transform;
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

                        randomVec2.x = transform.position.x + x;
                        randomVec2.y = transform.position.y + y;

                        objPool[i].gameObject.transform.position = randomVec2;
                        objPool[i].gameObject.SetActive(true);
                    }
                }

                yield return waitTime;
            }
        }
    }
}