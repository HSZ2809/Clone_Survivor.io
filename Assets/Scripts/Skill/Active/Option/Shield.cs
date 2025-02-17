using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Shield : ActiveSkill
    {
        [Header("Spac")]
        [SerializeField] private float coefficient = 0.0f;
        // [SerializeField] private float attackTerm = 1.0f;
        [SerializeField] private float range = 1.0f;

        [Header("Bullet")]
        [SerializeField] private Bullet_Shield prefab_bullet = null;
        [SerializeField] private Bullet_Shield magazine = null;

        public float BulletDamage { get { return coefficient * character.Atk; } }

        private void Start()
        {
            character.SetActiveSkill(this);

            ActiveSkillOn();
        }

        public void ActiveSkillOn()
        {
            Bullet_Shield bulletInstance = Instantiate(prefab_bullet, transform.position, transform.rotation);
            bulletInstance.Damage = BulletDamage;
            bulletInstance.transform.parent = transform;
            magazine = bulletInstance;
        }

        public override void Upgrade()
        {
            level += 1;

            switch (level)
            {
                case 2:
                    range *= 1.2f;
                    coefficient = 1.5f;
                    magazine.Damage = BulletDamage;
                    magazine.SetRange(range);
                    break;
                case 3:
                    range *= 1.2f;
                    coefficient = 2.0f;
                    magazine.Damage = BulletDamage;
                    magazine.SetRange(range);
                    break;
                case 4:
                    range *= 1.2f;
                    coefficient = 2.25f;
                    magazine.Damage = BulletDamage;
                    magazine.SetRange(range);
                    break;
                case 5:
                    range *= 2.5f;
                    coefficient = 10.0f;
                    magazine.Damage = BulletDamage;
                    magazine.SetRange(range);
                    break;
                case 6:
                    coefficient = 2.5f;
                    Debug.Log("Shield TryUpgrade() : evolution");
                    break;
                default:
                    Debug.LogWarning("Shield TryUpgrade() : invalid level");
                    break;
            }
        }
    }
}