using System;
using UnityEngine;

namespace ZUN
{
    public class EXPObjPool : MonoBehaviour
    {
        [SerializeField] EXPShard shardPerfab;

        EXPShard[] shards = new EXPShard[500];

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

            if(index ==  shards.Length)
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