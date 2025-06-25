using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

namespace ZUN
{
    public class Shotgun_Normal : ActiveSkillCtrl
    {
        [Space]
        [SerializeField] private AudioClip clip;
        Transform shootDir;

        AudioSource audioSource;
        Image reloadBar;

        [SerializeField] float coefficient;
        [SerializeField] float cooldown;
        [SerializeField] int magazineSize = 3;

        [Header("Bullet")]
        [SerializeField] Bullet_Shotgun bulletPrefab;
        IObjectPool<Bullet_Shotgun> objPool;

        public float BulletDamage { get { return coefficient * character.Atk; } }
        public float Cooldown { get { return cooldown * character.AtkSpeed; } }

        IEnumerator enumerator;
        readonly WaitForFixedUpdate waitForFixedUpdate = new();

        protected override void Awake()
        {
            base.Awake();

            objPool = new ObjectPool<Bullet_Shotgun>(CreateBullet, null, OnReleaseBullet, OnDestroyBullet, maxSize: 15);
            audioSource = GetComponent<AudioSource>();
            shootDir = character.GetMoveDir();
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
                int Angle = magazineSize / 2;

                audioSource.PlayOneShot(clip);

                for (int i = 0; i < magazineSize; i++)
                {
                    Bullet_Shotgun bullet = objPool.Get();
                    bullet.gameObject.transform.position = transform.position;
                    bullet.gameObject.transform.localRotation = shootDir.rotation;
                    bullet.gameObject.transform.Rotate(new Vector3(0, 0, 4 * (Angle - i)));
                    bullet.Damage = BulletDamage;
                    bullet.gameObject.SetActive(true);
                }

                for (float waitTime = 0.0f; waitTime < Cooldown; waitTime += Time.deltaTime)
                {
                    yield return waitForFixedUpdate;
                    reloadBar.fillAmount = waitTime / Cooldown;
                }
                reloadBar.fillAmount = 0.0f;
            }
        }

        public override void Upgrade(int level)
        {
            StopCoroutine(enumerator);

            switch (level)
            {
                case 2:
                    magazineSize = 6;
                    coefficient = 2;
                    break;
                case 3:
                    magazineSize = 9;
                    coefficient = 3;
                    break;
                case 4:
                    magazineSize = 13;
                    coefficient = 4;
                    break;
                case 5:
                    magazineSize = 15;
                    coefficient = 5;
                    break;
                default:
                    Debug.LogWarning("Shotgun_Normal TryUpgrade() : invalid level");
                    break;
            }

            StartCoroutine(enumerator);
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
