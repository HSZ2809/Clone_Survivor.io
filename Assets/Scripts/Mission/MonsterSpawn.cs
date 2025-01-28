using System;
using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class MonsterSpawn : MonoBehaviour
    {
        MissionCtrl missionCtrl;
        Character character;

        [Header("Monster")]
        [SerializeField] Monster mon1;
        Monster[] objPool;

        [Header("Amount of monsters")]
        [SerializeField] int level;
        [SerializeField] int[] addAmount;

        float minDistance = 16f;
        float maxDistance = 18f;
        
        IEnumerator enumerator;
        readonly WaitForSeconds waitTime = new (1.0f);

        private void Awake()
        {
            missionCtrl = GameObject.FindGameObjectWithTag("MissionCtrl").GetComponent<MissionCtrl>();
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        }

        private void Start()
        {
            enumerator = SummonMonster();
        }

        public void InitSet()
        {
            Array.Resize(ref objPool, addAmount[0]);

            for (int i = 0; i < objPool.Length; i++)
            {
                Monster mon = Instantiate(mon1, transform.position, transform.rotation);
                mon._MissionCtrl = missionCtrl;
                mon.CharacterTF = character.gameObject.transform;
                objPool[i] = mon;
            }

            level += 1;

            StartCoroutine(enumerator);
        }

        public void Setting()
        {
            StopCoroutine(enumerator);

            if(level >= addAmount.Length)
            {
                Debug.LogWarning("MonsterSpawn : invalid level");
                return;
            }

            int lengthBefore = objPool.Length;
            int lengthAfter = objPool.Length + addAmount[level];

            Array.Resize(ref objPool, lengthAfter);

            for(int i = lengthBefore; i < lengthAfter; i++)
            {
                Monster mon = Instantiate(mon1, transform.position, transform.rotation);
                mon._MissionCtrl = missionCtrl;
                mon.CharacterTF = character.gameObject.transform;
                objPool[i] = mon;
            }

            level += 1;

            StartCoroutine(enumerator);
        }

        IEnumerator SummonMonster()
        {
            Vector3 randomVec3;
            float randomAngle;
            float randomDistance;
            float x;
            float y;

            while (true)
            {
                for (int i = 0; i < objPool.Length; i++)
                {
                    randomAngle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
                    randomDistance = UnityEngine.Random.Range(minDistance, maxDistance);

                    x = Mathf.Cos(randomAngle) * randomDistance;
                    y = Mathf.Sin(randomAngle) * randomDistance;

                    randomVec3.x = transform.position.x + x;
                    randomVec3.y = transform.position.y + y;
                    randomVec3.z = 0.0f;

                    if (!objPool[i].gameObject.activeSelf)
                    {
                        objPool[i].gameObject.transform.position = randomVec3;
                        objPool[i].Hp = 100;
                        objPool[i].gameObject.SetActive(true);
                    }
                }

                yield return waitTime;
            }
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawWireSphere(transform.position, minDistance);

        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawWireSphere(transform.position, maxDistance);
        //}
    }
}