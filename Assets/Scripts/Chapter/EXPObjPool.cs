using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace ZUN
{
    public class EXPObjPool : MonoBehaviour
    {
        [SerializeField] EXPShard shardPerfab;

        EXPShard[] shards = new EXPShard[500];

        private void Start()
        {
            for(int i = 0; i < 10; i++)
            {
                AddShard(i);
                shards[i].SetType(EXPShard.Type.SMALL);

                Vector3 randomVec3;
                float randomAngle;
                float randomDistance;
                float x;
                float y;

                randomAngle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
                randomDistance = UnityEngine.Random.Range(7.0f, 12.0f);

                x = Mathf.Cos(randomAngle) * randomDistance;
                y = Mathf.Sin(randomAngle) * randomDistance;

                randomVec3.x = transform.position.x + x;
                randomVec3.y = transform.position.y + y;
                randomVec3.z = 0.0f;

                shards[i].gameObject.transform.position = randomVec3;
                shards[i].gameObject.SetActive(true);
            }
        }

        public void SetShard(EXPShard.Type type, Vector3 pos)
        {
            int index = 0;

            while (index < shards.Length)
            {
                if (shards[index] == null)
                {
                    AddShard(index);
                    break;
                }
                
                if(!shards[index].gameObject.activeSelf)
                {
                    if (shards[index].ShardType != type)
                    break;
                }

                index++;
            }

            if(index == shards.Length)
            {
                Array.Resize(ref shards, shards.Length * 2);
                AddShard(index);
            }

            if(shards[index].ShardType != type)
                shards[index].SetType(type);

            shards[index].gameObject.transform.position = pos;
            shards[index].gameObject.SetActive(true);
        }

        private void AddShard(int index)
        {
            EXPShard instant = Instantiate(shardPerfab);
            shards[index] = instant;
        }
    }
}