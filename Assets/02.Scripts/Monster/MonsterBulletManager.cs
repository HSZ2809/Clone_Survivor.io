using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class MonsterBulletManager : MonoBehaviour
    {
        public IObjectPool<MonsterBullet> BulletPool { get; private set; }
        [SerializeField] MonsterBullet monsterBullet;

        private void Awake()
        {
            BulletPool = new ObjectPool<MonsterBullet>(CreateBullet, null, OnReleaseBullet, OnDestroyBullet, maxSize: 30);
        }

        MonsterBullet CreateBullet()
        {
            MonsterBullet bullet = Instantiate(monsterBullet);
            bullet.SetBulletPool(BulletPool);
            return bullet;
        }

        void OnReleaseBullet(MonsterBullet mb)
        {
            mb.gameObject.SetActive(false);
        }

        void OnDestroyBullet(MonsterBullet mb)
        {
            Destroy(mb.gameObject);
        }
    }
}