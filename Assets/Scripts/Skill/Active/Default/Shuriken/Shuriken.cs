using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

namespace ZUN
{
    public class Shuriken : ActiveSkill
    {
        [Space]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip clip;
        [SerializeField] private SpriteRenderer handSprite;
        [SerializeField] private Transform shootDir;
        [SerializeField] private Image reloadBar;

        [Header("Spac")]
        [SerializeField] private float coefficient;
        [SerializeField] private float cooldown;
        [SerializeField] private int magazineSize;
        readonly float findRange = 10.0f;
        LayerMask monsterLayer;

        [Header("Bullet")]
        [SerializeField] private Bullet_Shuriken bulletPrefab;
        IObjectPool<Bullet_Shuriken> objPool;

        public float BulletDamage { get { return coefficient * character.Atk; } }
        public float Cooldown { get { return cooldown * character.AtkSpeed; } }

        IEnumerator enumerator;
        readonly WaitForFixedUpdate waitForFixedUpdate = new();
        readonly WaitForSeconds firerate = new(0.1f);

        private void Start()
        {
            objPool = new ObjectPool<Bullet_Shuriken>(CreateBullet, null, OnReleaseBullet, OnDestroyBullet, maxSize: 5);

            handSprite.sortingLayerName = "Weapon";
            monsterLayer = (1 << LayerMask.NameToLayer("Monster"));
            reloadBar = character.ReloadBar();
            enumerator = Shoot();
            character.SetActiveSkill(this);

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

                for(float waitTime = 0.0f; waitTime < Cooldown; waitTime += Time.deltaTime)
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

        public override void Upgrade()
        {
            StopCoroutine(enumerator);

            level += 1;

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
                    break; ;
                case 5:
                    magazineSize = 5;
                    coefficient = 5.6f;
                    break;
                case 6:
                    // 무기 교체 로직 필요
                    // coefficient = 5.6f;
                    Debug.Log("Shuriken TryUpgrade() : evolution");
                    break;
                default:
                    Debug.LogWarning("Shuriken TryUpgrade() : invalid level");
                    break;
            }

            StartCoroutine(enumerator);
        }

        Bullet_Shuriken CreateBullet()
        {
            Bullet_Shuriken bullet = Instantiate(bulletPrefab);
            bullet.SetBulletPool(objPool);
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