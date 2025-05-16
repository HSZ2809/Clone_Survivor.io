using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class Soccerball_Normal : SkillCtrl
    {
        [Header("Spac")]
        [SerializeField] private float coefficient;
        [SerializeField] private float cooldown;
        [SerializeField] private int magazineSize;
        [SerializeField] private float moveSpeed;

        [Header("Bullet")]
        [SerializeField] private Bullet_Soccerball_Normal bulletPrefab = null;

        IObjectPool<Bullet_Soccerball_Normal> objPool;
        IEnumerator enumerator;

        public float BulletDamage { get { return coefficient * character.Atk; } }

        protected override void Awake()
        {
            base.Awake();

            objPool = new ObjectPool<Bullet_Soccerball_Normal>(CreateBullet, null, OnReleaseBullet, OnDestroyBullet, maxSize: 5);
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
                    Bullet_Soccerball_Normal bullet = objPool.Get();
                    bullet.gameObject.transform.position = transform.position;
                    bullet.Damage = BulletDamage;
                    bullet.MoveSpeed = moveSpeed;
                    bullet.CharMoveSpeed = character.MoveSpeed;
                    bullet.gameObject.SetActive(true);
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
                    break;
                case 3:
                    moveSpeed += 3;
                    coefficient = 8.0f;
                    break;
                case 4:
                    moveSpeed += 3;
                    coefficient = 10.0f;
                    break;
                case 5:
                    magazineSize += 1;
                    break;
                case 6:
                    Debug.Log("Soccerball TryUpgrade() : evolution");
                    break;
                default:
                    Debug.LogWarning("Soccerball TryUpgrade() : invalid level");
                    break;
            }

            StartCoroutine(enumerator);
        }

        Bullet_Soccerball_Normal CreateBullet()
        {
            Bullet_Soccerball_Normal bullet = Instantiate(bulletPrefab);
            bullet.SetBulletPool(ref objPool);
            bullet.InitializeSpriteAlpha(manager_VisualEffect.IsEffectReduced);
            return bullet;
        }

        void OnReleaseBullet(Bullet_Soccerball_Normal bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        void OnDestroyBullet(Bullet_Soccerball_Normal bullet)
        {
            Destroy(bullet.gameObject);
        }
    }
}
