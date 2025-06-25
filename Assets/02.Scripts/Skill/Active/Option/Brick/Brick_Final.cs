using System.Collections;
using UnityEngine;


namespace ZUN
{
    public class Brick_Finall : ActiveSkillCtrl
    {
        [Space]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip clip;

        [Header("Spac")]
        [SerializeField] private float coefficient;
        [SerializeField] private float cooldown;
        [SerializeField] private int magazineSize;

        [Header("Bullet")]
        [SerializeField] private Bullet_Brick_Fianl bulletPrefab;

        Bullet_Brick_Fianl[] objPool;
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

        IEnumerator Shoot()
        {
            while (true)
            {
                for (int i = 0; i < magazineSize; i++)
                {
                    objPool[i].gameObject.transform.position = transform.position;
                    objPool[i].Damage = BulletDamage;
                    objPool[i].gameObject.SetActive(true);
                }

                audioSource.PlayOneShot(clip);

                yield return new WaitForSeconds(cooldown * character.AtkSpeed);
            }
        }

        void SetAngle()
        {
            objPool = new Bullet_Brick_Fianl[magazineSize];

            for (int i = 0; i < objPool.Length; i++)
            {
                float angle = i * (360.0f / objPool.Length);
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Bullet_Brick_Fianl bulletInstance = Instantiate(bulletPrefab, transform.position, rotation);
                objPool[i] = bulletInstance;
                bulletInstance.InitializeSpriteAlpha(manager_VisualEffect.IsEffectReduced);
                bulletInstance.Damage = BulletDamage;
            }
        }

        public override void Upgrade(int level)
        {
            Debug.LogWarning("Brick_Final TryUpgrade() : invalid level (" + level + ")");
        }
    }
}