using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

namespace ZUN
{
    public class ObjectPool_ExpShard : MonoBehaviour
    {
        public IObjectPool<EXPShard> Pool { get; private set; }
        [SerializeField] EXPShard perfab;
        [SerializeField] int initialValue = 10;

        private void Awake()
        {
            Pool = new ObjectPool<EXPShard>(CreateShard, null, OnReleaseShard, OnDestroyShard, maxSize: 500);
        }

        private void Start()
        {
            InitShard(initialValue);
        }

        public void InitShard(int _initialValue)
        {
            for (int i = 0; i < _initialValue; i++)
            {
                EXPShard shard = Pool.Get();
                SceneManager.MoveGameObjectToScene(shard.gameObject, gameObject.scene);
                shard.SetType(EXPShard.Type.SMALL);

                float randomAngle = Random.Range(0f, 2f * Mathf.PI);
                float randomDistance = Random.Range(7.0f, 12.0f);

                float x = Mathf.Cos(randomAngle) * randomDistance;
                float y = Mathf.Sin(randomAngle) * randomDistance;

                Vector2 randomVec2 = new (transform.position.x + x, transform.position.y + y);

                shard.gameObject.transform.position = randomVec2;
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