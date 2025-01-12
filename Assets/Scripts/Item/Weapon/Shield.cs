using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Shield : Weapon
    {
        [Header("Spac")]
        [SerializeField] private float defaultDamage = 0.0f;
        [SerializeField] private float attackTerm = 1.0f;
        [SerializeField] private float range = 1.0f;

        [Header("Magazine")]
        [SerializeField] private Bullet_Shield bullet = null;
        [SerializeField] private Bullet_Shield magazine = null;

        public float BulletDamage { get { return defaultDamage + character.AttackPower; } }

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        }

        public override void ActivateWeapon()
        {
            Bullet_Shield bulletInstance = Instantiate(bullet, transform.position, transform.rotation);
            bulletInstance.Damage = BulletDamage;
            bulletInstance.transform.parent = transform;
            magazine = bulletInstance;
        }
    }
}