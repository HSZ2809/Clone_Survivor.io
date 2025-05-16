using UnityEngine;

namespace ZUN
{
    public class Shield_Final : SkillCtrl
    {
        [Header("Spac")]
        [SerializeField] float coefficient;
        [SerializeField] float range;

        [Header("Bullet")]
        [SerializeField] Bullet_Shield_Final prefab_bullet;
        // Bullet_Shield_Normal magazine = null;

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
            Bullet_Shield_Final bulletInstance = Instantiate(prefab_bullet, transform.position, transform.rotation);
            bulletInstance.Damage = BulletDamage;
            bulletInstance.transform.parent = transform;
            bulletInstance.InitializeSpriteAlpha(manager_VisualEffect.IsEffectReduced);
            bulletInstance.SetRange(range);
            // magazine = bulletInstance;
        }

        public override void Upgrade(int level)
        {
            switch (level)
            {
                default:
                    Debug.LogWarning("Shield_Final TryUpgrade()");
                    break;
            }
        }
    }
}
