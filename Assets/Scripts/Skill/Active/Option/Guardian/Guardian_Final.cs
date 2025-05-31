using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Guardian_Final : ActiveSkillCtrl
    {
        [Space]
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip clip;
        [SerializeField] Transform pivot;

        [Header("Spac")]
        [SerializeField] float coefficient;
        [SerializeField] float cooldown;
        [SerializeField] float rotationSpeed;
        [SerializeField] float range;
        [SerializeField] float duration;
        [SerializeField] int magazineSize;

        [Header("Bullet")]
        [SerializeField] Bullet_Guardian_Final bulletPrefab;

        Bullet_Guardian_Final[] objPool;
        IEnumerator enumerator;

        public float BulletDamage { get { return coefficient * character.Atk; } }

        protected override void Awake()
        {
            base.Awake();

            enumerator = Shoot();
            SetAngle();
        }

        private void OnEnable()
        {
            StartCoroutine(enumerator);
        }

        private void Update()
        {
            pivot.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        IEnumerator Shoot()
        {
            while (true)
            {
                yield return null;

                pivot.gameObject.SetActive(true);

                audioSource.PlayOneShot(clip);

                yield return new WaitForSeconds(duration);

                pivot.gameObject.SetActive(false);

                yield return new WaitForSeconds(cooldown * character.AtkSpeed);
            }
        }

        void SetAngle()
        {
            objPool = new Bullet_Guardian_Final[magazineSize];
            float angle = (360f / magazineSize) * Mathf.Deg2Rad;

            for (int i = 0; i < objPool.Length; i++)
            {
                Bullet_Guardian_Final bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bulletInstance.transform.parent = pivot.transform;
                bulletInstance.InitializeSpriteAlpha(manager_VisualEffect.IsEffectReduced);
                objPool[i] = bulletInstance;

                float x = Mathf.Cos(angle * i);
                float y = Mathf.Sin(angle * i);
                Vector3 temp = new(transform.position.x + (x * range), transform.position.y + (y * range), 0);

                bulletInstance.gameObject.transform.position = temp;
                bulletInstance.Damage = BulletDamage;
            }
        }

        public override void Upgrade(int level)
        {
            switch (level)
            {
                default:
                    Debug.LogWarning("Guardian_Final TryUpgrade() : invalid level (" + level + ")");
                    break;
            }
        }
    }
}