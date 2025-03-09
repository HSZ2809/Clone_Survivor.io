using System;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class ObjectPool_ExpShard : MonoBehaviour
    {
        public IObjectPool<EXPShard> Pool { get; private set; }
        [SerializeField] EXPShard perfab;

        private void Awake()
        {
            Pool = new ObjectPool<EXPShard>(CreateShard, null, OnReleaseShard, OnDestroyShard, maxSize: 500);
        }

        public void InitShard()
        {
            for(int i = 0; i < 10; i++)
            {
                EXPShard shard = Pool.Get();
                shard.SetType(EXPShard.Type.SMALL);

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

                shard.gameObject.transform.position = randomVec3;
                shard.gameObject.SetActive(true);
            }
        }

        EXPShard CreateShard()
        {
            EXPShard shard = Instantiate(perfab);
            shard.SetPool(Pool);
            return shard;
        }

        void OnReleaseShard(EXPShard shard)
        {
            shard.gameObject.SetActive(false);
        }

        void OnDestroyShard(EXPShard shard)
        {
            Destroy(shard.gameObject);
        }

        public void SetShard(EXPShard.Type type, Vector3 pos)
        {
            EXPShard shard = Pool.Get();

            if (shard.ShardType != type)
                shard.SetType(type);

            shard.gameObject.transform.position = pos;
            shard.gameObject.SetActive(true);
        }
    }
}