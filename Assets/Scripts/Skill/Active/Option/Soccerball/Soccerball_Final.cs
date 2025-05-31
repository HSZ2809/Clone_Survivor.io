using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class Soccerball_Final : ActiveSkillCtrl
    {
        [Header("Spac")]
        [SerializeField] private float coefficient;
        [SerializeField] private float cooldown;
        [SerializeField] private int magazineSize;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float duration;
        public float BulletDamage { get { return coefficient * character.Atk; } }
        public bool IsDurationEnd { get; private set; }

        [Header("Bullet")]
        [SerializeField] private Bullet_Soccerball_Final bulletPrefab = null;

        public IObjectPool<Bullet_Soccerball_Final> ObjPool { get; private set; }
        IEnumerator enumerator;


        protected override void Awake()
        {
            base.Awake();

            ObjPool = new ObjectPool<Bullet_Soccerball_Final>(CreateBullet, null, OnReleaseBullet, OnDestroyBullet, maxSize: 30);
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
                StartCoroutine(DurationCheck());

                for (int i = 0; i < magazineSize; i++)
                {
                    ShootBullet(transform);
                }

                yield return new WaitForSeconds(cooldown * character.AtkSpeed);
            }
        }

        public void ShootBullet(Transform spawnTransform)
        {
            Bullet_Soccerball_Final bullet = ObjPool.Get();
            bullet.gameObject.transform.position = spawnTransform.position;
            bullet.Damage = BulletDamage;
            bullet.MoveSpeed = moveSpeed;
            bullet.CharMoveSpeed = character.MoveSpeed;
            bullet.SetSkill(this);
            bullet.gameObject.SetActive(true);
        }

        IEnumerator DurationCheck()
        {
            IsDurationEnd = false;

            yield return new WaitForSeconds(duration);

            IsDurationEnd = true;
        }

        public override void Upgrade(int level)
        {
            switch (level)
            {
                default:
                    Debug.LogWarning("Soccerball_Final TryUpgrade()");
                    break;
            }
        }

        Bullet_Soccerball_Final CreateBullet()
        {
            Bullet_Soccerball_Final bullet = Instantiate(bulletPrefab);
            bullet.InitializeSpriteAlpha(manager_VisualEffect.IsEffectReduced);
            return bullet;
        }

        void OnReleaseBullet(Bullet_Soccerball_Final bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        void OnDestroyBullet(Bullet_Soccerball_Final bullet)
        {
            Destroy(bullet.gameObject);
        }
    }
}