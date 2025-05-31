using UnityEngine;

namespace ZUN
{
    public class Shield_Normal : ActiveSkillCtrl
    {
        [Header("Spac")]
        [SerializeField] float coefficient;
        [SerializeField] float range;

        [Header("Bullet")]
        [SerializeField] Bullet_Shield_Normal prefab_bullet;
        Bullet_Shield_Normal magazine;

        public float BulletDamage { get { return coefficient * character.Atk; } }

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            ActiveSkillOn();
        }

        public void ActiveSkillOn()
        {
            Bullet_Shield_Normal bulletInstance = Instantiate(prefab_bullet, transform.position, transform.rotation);
            bulletInstance.Damage = BulletDamage;
            bulletInstance.transform.parent = transform;
            bulletInstance.InitializeSpriteAlpha(manager_VisualEffect.IsEffectReduced);
            bulletInstance.SetRange(range);
            magazine = bulletInstance;
        }

        public override void Upgrade(int level)
        {
            switch (level)
            {
                case 2:
                    range *= 1.4f;
                    coefficient = 1.5f;
                    magazine.Damage = BulletDamage;
                    magazine.SetRange(range);
                    break;
                case 3:
                    range *= 1.4f;
                    coefficient = 2.0f;
                    magazine.Damage = BulletDamage;
                    magazine.SetRange(range);
                    break;
                case 4:
                    range *= 1.4f;
                    coefficient = 2.25f;
                    magazine.Damage = BulletDamage;
                    magazine.SetRange(range);
                    break;
                case 5:
                    range *= 1.4f;
                    coefficient = 10.0f;
                    magazine.Damage = BulletDamage;
                    magazine.SetRange(range);
                    break;
                default:
                    Debug.LogWarning("Shield TryUpgrade() : invalid level");
                    break;
            }
        }
    }
}