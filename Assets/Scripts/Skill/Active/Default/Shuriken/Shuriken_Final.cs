using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class Shuriken_Final : ActiveSkillCtrl
    {
        private AudioSource audioSource;
        [SerializeField] private AudioClip clip;
        [SerializeField] private Transform shootDir;

        [Header("Spac")]
        [SerializeField] private float coefficient;
        readonly float findRange = 10.0f;
        LayerMask monsterLayer;

        [Header("Bullet")]
        [SerializeField] private Bullet_Shuriken bulletPrefab;
        IObjectPool<Bullet_Shuriken> objPool;

        public float BulletDamage { get { return coefficient * character.Atk; } }

        IEnumerator enumerator;
        readonly WaitForFixedUpdate waitForFixedUpdate = new();
        readonly WaitForSeconds firerate = new(0.1f);

        protected override void Awake()
        {
            base.Awake();

            objPool = new ObjectPool<Bullet_Shuriken>(CreateBullet, null, OnReleaseBullet, OnDestroyBullet, maxSize: 30);
            audioSource = GetComponent<AudioSource>();
            monsterLayer = (1 << LayerMask.NameToLayer("Target"));
            character.ReloadBar().gameObject.SetActive(false);
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

                SetAim();

                Bullet_Shuriken bullet = objPool.Get();
                bullet.gameObject.transform.position = shootDir.position;
                bullet.gameObject.transform.localRotation = shootDir.rotation;
                bullet.Damage = BulletDamage;
                bullet.gameObject.SetActive(true);

                audioSource.PlayOneShot(clip);
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

        Bullet_Shuriken CreateBullet()
        {
            Bullet_Shuriken bullet = Instantiate(bulletPrefab);
            bullet.SetBulletPool(objPool);
            bullet.InitializeSpriteAlpha(manager_VisualEffect.IsEffectReduced);
            return bullet;
        }

        void OnReleaseBullet(Bullet_Shuriken bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        void OnDestroyBullet(Bullet_Shuriken bullet)
        {
            Destroy(bullet.gameObject);
        }
    }
}