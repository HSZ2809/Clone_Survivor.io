using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class Rocket_Normal : SkillCtrl
    {
        [Space]
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip clip;
        [SerializeField] Transform shootDir;

        [Header("Spac")]
        [SerializeField] float coefficient;
        [SerializeField] float cooldown;
        [SerializeField] int magazineSize;
        [SerializeField] float firerate;
        readonly float findRange = 10.0f;
        LayerMask monsterLayer;

        [Header("Bullet")]
        [SerializeField] Bullet_Rocket_Normal bulletPrefab;

        IObjectPool<Bullet_Rocket_Normal> objPool;
        IEnumerator enumerator;

        public float BulletDamage { get { return coefficient * character.Atk; } }

        protected override void Awake()
        {
            base.Awake();

            objPool = new ObjectPool<Bullet_Rocket_Normal>(CreateBullet, null, OnReleaseBullet, OnDestroyBullet, maxSize: 3);
            monsterLayer = (1 << LayerMask.NameToLayer("Target"));
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

                    SetAim();

                    Bullet_Rocket_Normal bullet = objPool.Get();
                    bullet.gameObject.transform.position = shootDir.position;
                    bullet.gameObject.transform.localRotation = shootDir.rotation;
                    bullet.Damage = BulletDamage;
                    bullet.gameObject.SetActive(true);

                    audioSource.PlayOneShot(clip);
                }

                yield return new WaitForSeconds(cooldown * character.AtkSpeed);
            }
        }

        private void SetAim()
        {
            Vector3 aim;

            Collider2D[] monsterCol;
            monsterCol = Physics2D.OverlapCircleAll(transform.position, findRange, monsterLayer);

            if (monsterCol.Length < 1)
                monsterCol = Physics2D.OverlapCircleAll(transform.position, findRange * 2, monsterLayer);

            if (monsterCol.Length < 1)
                monsterCol = Physics2D.OverlapCircleAll(transform.position, findRange * 3, monsterLayer);

            if (monsterCol.Length > 0)
            {
                Transform nearestMon = monsterCol[0].transform;
                float distance = Vector3.Distance(transform.position, nearestMon.position);

                foreach (Collider2D col in monsterCol)
                {
                    float compareDis = Vector3.Distance(transform.position, col.transform.position);

                    if (compareDis < distance)
                    {
                        nearestMon = col.transform;
                        distance = compareDis;
                    }
                }

                aim = (transform.position - nearestMon.position).normalized;
                float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
                shootDir.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            }
        }

        public override void Upgrade(int level)
        {
            StopCoroutine(enumerator);

            switch (level)
            {
                case 2:
                    coefficient = 4;
                    break;
                case 3:
                    magazineSize += 1;
                    coefficient = 4.0f;
                    break;
                case 4:
                    coefficient = 6.0f;
                    break; ;
                case 5:
                    magazineSize += 1;
                    break;
                default:
                    Debug.LogWarning("Rocket Upgrade() : invalid level (" + level + ")");
                    break;
            }

            StartCoroutine(enumerator);
        }

        Bullet_Rocket_Normal CreateBullet()
        {
            Bullet_Rocket_Normal bullet = Instantiate(bulletPrefab);
            bullet.SetBulletPool(objPool);
            bullet.InitializeSpriteAlpha(manager_VisualEffect.IsEffectReduced);
            return bullet;
        }

        void OnReleaseBullet(Bullet_Rocket_Normal bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        void OnDestroyBullet(Bullet_Rocket_Normal bullet)
        {
            Destroy(bullet.gameObject);
        }
    }
}
