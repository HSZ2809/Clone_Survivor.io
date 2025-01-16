using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Shield : Weapon
    {
        [Header("Spac")]
        [SerializeField] private float damage = 0.0f;
        // [SerializeField] private float attackTerm = 1.0f;
        [SerializeField] private float range = 1.0f;

        [Header("Bullet")]
        [SerializeField] private Bullet_Shield prefab_bullet = null;
        [SerializeField] private Bullet_Shield magazine = null;

        public float BulletDamage { get { return damage + character.AttackPower; } }

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        }

        public override void ActivateWeapon()
        {
            Bullet_Shield bulletInstance = Instantiate(prefab_bullet, transform.position, transform.rotation);
            bulletInstance.Damage = BulletDamage;
            bulletInstance.transform.parent = transform;
            magazine = bulletInstance;
        }

        public override bool TryUpgrade(int level)
        {
            switch(level)
            {
                case 2:
                    range *= 1.2f;
                    damage += 10.0f;
                    magazine.Damage = BulletDamage;
                    magazine.SetRange(range);
                    break;
                case 3:
                    range *= 1.2f;
                    damage += 10.0f;
                    magazine.Damage = BulletDamage;
                    magazine.SetRange(range);
                    break;
                case 4:
                    range *= 1.2f;
                    damage += 10.0f;
                    magazine.Damage = BulletDamage;
                    magazine.SetRange(range);
                    break;
                case 5:
                    range *= 1.2f;
                    damage += 10.0f;
                    magazine.Damage = BulletDamage;
                    magazine.SetRange(range);
                    break;
                case 6:
                    Debug.Log("Shield TryUpgrade() : evolution");
                    break;
                default:
                    Debug.LogWarning("Shield TryUpgrade() : invalid level");
                    return false;
            }

            return true;
        }
    }
}