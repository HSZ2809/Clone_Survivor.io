using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

namespace ZUN
{
    public class Shuriken_Normal : ActiveSkillCtrl
    {
        [SerializeField] private AudioClip clip;
        [SerializeField] private Transform shootDir;

        private AudioSource audioSource;
        private Image reloadBar;

        [Header("Spac")]
        [SerializeField] private float coefficient;
        private float reinforcement = 1.0f;
        [SerializeField] private float cooldown;
        [SerializeField] private int magazineSize;
        readonly float findRange = 10.0f;
        LayerMask monsterLayer;

        [Header("Bullet")]
        [SerializeField] private Bullet_Shuriken bulletPrefab;
        IObjectPool<Bullet_Shuriken> objPool;

        public float BulletDamage { get { return reinforcement * coefficient * character.Atk; } }
        public float Cooldown { get { return cooldown * character.AtkSpeed; } }

        IEnumerator enumerator;
        readonly WaitForFixedUpdate waitForFixedUpdate = new();
        readonly WaitForSeconds firerate = new(0.1f);

        protected override void Awake()
        {
            base.Awake();

            objPool = new ObjectPool<Bullet_Shuriken>(CreateBullet, null, OnReleaseBullet, OnDestroyBullet, maxSize: 5);
            audioSource = GetComponent<AudioSource>();
            monsterLayer = (1 << LayerMask.NameToLayer("Target"));
            reloadBar = character.ReloadBar();
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
                    yield return firerate;

                    SetAim();

                    Bullet_Shuriken bullet = objPool.Get();
                    bullet.gameObject.transform.position = shootDir.position;
                    bullet.gameObject.transform.localRotation = shootDir.rotation;
                    bullet.Damage = BulletDamage;
                    bullet.gameObject.SetActive(true);

                    audioSource.PlayOneShot(clip);
                }

                for (float waitTime = 0.0f; waitTime < Cooldown; waitTime += Time.deltaTime)
                {
                    yield return waitForFixedUpdate;
                    reloadBar.fillAmount = waitTime / Cooldown;
                }
                reloadBar.fillAmount = 0.0f;
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
                    magazineSize = 2;
                    coefficient = 2;
                    break;
                case 3:
                    magazineSize = 3;
                    coefficient = 3;
                    break;
                case 4:
                    magazineSize = 4;
                    coefficient = 4;
                    break;
                case 5:
                    magazineSize = 5;
                    coefficient = 5.6f;
                    break;
                default:
                    Debug.LogWarning("Shuriken TryUpgrade() : invalid level");
                    break;
            }

            StartCoroutine(enumerator);
        }

        //void TierUpgrade(EquipmentTier tier)
        //{
        //    if (tier >= EquipmentTier.Rare)
        //        reinforcement = 1.3f;
        //    if (tier >= EquipmentTier.Elite)
        //        Upgrade(2);
        //    if (tier >= EquipmentTier.Legend)
        //        Debug.Log("갈라지는 효과");
        //}

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