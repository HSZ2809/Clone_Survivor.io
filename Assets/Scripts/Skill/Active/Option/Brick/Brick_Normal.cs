using System.Collections;
using UnityEngine;
using UnityEngine.Pool;


namespace ZUN
{
    public class Brick_Normal : SkillCtrl
    {
        [Space]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip clip;

        [Header("Spac")]
        [SerializeField] private float coefficient;
        [SerializeField] private float cooldown;
        [SerializeField] private float firerate;
        [SerializeField] private int magazineSize;

        [Header("Bullet")]
        [SerializeField] private Bullet_Brick_Normal bulletPrefab;

        IObjectPool<Bullet_Brick_Normal> objPool;
        IEnumerator enumerator;

        public float BulletDamage { get { return coefficient * character.Atk; } }

        protected override void Awake()
        {
            base.Awake();

            objPool = new ObjectPool<Bullet_Brick_Normal>(CreateBullet, null, OnReleaseBullet, OnDestroyBullet, maxSize:5);
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
                for (int i = 0; i < magazineSize; i++)
                {
                    yield return new WaitForSeconds(firerate);

                    Bullet_Brick_Normal bullet = objPool.Get();
                    bullet.gameObject.transform.position = transform.position;
                    bullet.Damage = BulletDamage;
                    bullet.gameObject.SetActive(true);

                    audioSource.PlayOneShot(clip);
                }

                yield return new WaitForSeconds(cooldown * character.AtkSpeed);
            }
        }

        public override void Upgrade(int level)
        {
            StopCoroutine(enumerator);

            switch (level)
            {
                case 2:
                    magazineSize += 1;
                    coefficient = 3;
                    break;
                case 3:
                    magazineSize += 1;
                    coefficient = 4;
                    break;
                case 4:
                    magazineSize += 1;
                    coefficient = 5;
                    break;
                case 5:
                    magazineSize += 1;
                    coefficient = 8;
                    break;
                default:
                    Debug.LogWarning("Brick TryUpgrade() : invalid level (" + level + ")");
                    break;
            }

            StartCoroutine(enumerator);
        }

        Bullet_Brick_Normal CreateBullet()
        {
            Bullet_Brick_Normal bullet = Instantiate(bulletPrefab);
            bullet.SetBulletPool(objPool);
            bullet.InitializeSpriteAlpha(manager_VisualEffect.IsEffectReduced);
            return bullet;
        }

        void OnReleaseBullet(Bullet_Brick_Normal bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        void OnDestroyBullet(Bullet_Brick_Normal bullet)
        {
            Destroy(bullet.gameObject);
        }
    }
}