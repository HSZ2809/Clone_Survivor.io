using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Guardian_Normal : ActiveSkillCtrl
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
        [SerializeField] Bullet_Guardian_Normal bulletPrefab;

        readonly List<Bullet_Guardian_Normal> objPool = new();
        IEnumerator enumerator;

        public float BulletDamage { get { return coefficient * character.Atk; } }

        protected override void Awake()
        {
            base.Awake();

            enumerator = Shoot();
        }

        private void OnEnable()
        {
            SetAngle();
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

                foreach (var obj in objPool)
                    obj.BulletEnable();

                audioSource.PlayOneShot(clip);

                yield return new WaitForSeconds(duration);

                foreach (var obj in objPool)
                    obj.BulletDisable();

                yield return new WaitForSeconds(cooldown * character.AtkSpeed);
            }
        }

        void SetAngle()
        {
            StopCoroutine(enumerator);

            while (magazineSize > objPool.Count)
            {
                Bullet_Guardian_Normal bulletInstance = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bulletInstance.transform.parent = pivot.transform;
                bulletInstance.InitializeSpriteAlpha(manager_VisualEffect.IsEffectReduced);
                objPool.Add(bulletInstance);
            }

            float angle = (360f / magazineSize) * Mathf.Deg2Rad;

            for (int i = 0; i < objPool.Count; i++)
            {
                float x = Mathf.Cos(angle * i);
                float y = Mathf.Sin(angle * i);
                Vector3 temp = new(transform.position.x + (x * range), transform.position.y + (y * range), 0);

                objPool[i].gameObject.transform.position = temp;
                objPool[i].Damage = BulletDamage;
            }

            StartCoroutine(enumerator);
        }

        public override void Upgrade(int level)
        {
            switch (level)
            {
                case 2:
                    magazineSize += 1;
                    coefficient = 0.6f;
                    cooldown = 8.0f;
                    break;
                case 3:
                    magazineSize += 1;
                    coefficient = 0.7f;
                    cooldown = 6.0f;
                    break;
                case 4:
                    magazineSize += 1;
                    coefficient = 0.8f;
                    cooldown = 4.0f;
                    break;
                case 5:
                    magazineSize += 1;
                    coefficient = 0.9f;
                    cooldown = 2.0f;
                    break;
                default:
                    Debug.LogWarning("Guardian TryUpgrade() : invalid level (" + level + ")");
                    break;
            }

            SetAngle();
        }
    }
}