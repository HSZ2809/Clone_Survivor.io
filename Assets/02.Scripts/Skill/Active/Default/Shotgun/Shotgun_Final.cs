using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class Shotgun_Final : ActiveSkillCtrl
    {
        // [SerializeField] AudioClip clip;
        Transform shootDir;

        // AudioSource audioSource;

        [SerializeField] float coefficient;

        [Header("Bullet")]
        [SerializeField] Bullet_Shotgun bulletPrefab;
        IObjectPool<Bullet_Shotgun> objPool;

        public float BulletDamage { get { return coefficient * character.Atk; } }

        IEnumerator enumerator;
        readonly WaitForSeconds firerate = new(0.1f);

        protected override void Awake()
        {
            base.Awake();

            objPool = new ObjectPool<Bullet_Shotgun>(CreateBullet, null, OnReleaseBullet, OnDestroyBullet, maxSize: 100);
            // audioSource = GetComponent<AudioSource>();
            shootDir = character.GetMoveDir();
            enumerator = Shoot();
        }

        private void OnEnable()
        {
            StartCoroutine(enumerator);
        }

        IEnumerator Shoot()
        {
            while (true)
            {
                yield return firerate;

                Bullet_Shotgun bullet = objPool.Get();
                bullet.gameObject.transform.position = transform.position;
                bullet.gameObject.transform.localRotation = shootDir.rotation;
                bullet.Damage = BulletDamage;
                bullet.gameObject.SetActive(true);
            }
        }

        public override void Upgrade(int level)
        {
            switch (level)
            {
                case 6:
                    coefficient = 5.6f;
                    break;
                default:
                    Debug.LogWarning("Shuriken TryUpgrade() : invalid level");
                    break;
            }
        }

        Bullet_Shotgun CreateBullet()
        {
            Bullet_Shotgun bullet = Instantiate(bulletPrefab);
            bullet.SetBulletPool(objPool);
            bullet.InitializeSpriteAlpha(manager_VisualEffect.IsEffectReduced);
            return bullet;
        }

        void OnReleaseBullet(Bullet_Shotgun bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        void OnDestroyBullet(Bullet_Shotgun bullet)
        {
            Destroy(bullet.gameObject);
        }
    }
}