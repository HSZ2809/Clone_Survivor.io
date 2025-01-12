using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Shotgun : Weapon
    {
        [SerializeField] private Transform shootDir = null;

        [SerializeField] private float defaultDamage = 0.0f;
        [SerializeField] private float reloadTime = 1.0f;

        [Header("Magazine")]
        [SerializeField] private Bullet_Shotgun bullet = null;
        [SerializeField] private List<Bullet_Shotgun> magazine = null;

        public float BulletDamage { get{ return defaultDamage + character.AttackPower; } }
        
        IEnumerator enumerator;

        private void Awake() 
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            shootDir = character.GetShootDirection();
        }

        public override void ActivateWeapon()
        {
            enumerator = Shoot();
            StartCoroutine(enumerator);
        }

        IEnumerator Shoot()
        {
            while (true)
            {
                yield return new WaitForSeconds(reloadTime * character.AttackSpeed);

                bool bulletFound = false;

                for (int i = 0; i < magazine.Count; i++)
                {
                    if (!magazine[i].gameObject.activeSelf)
                    {
                        magazine[i].gameObject.transform.position = transform.position;
                        magazine[i].gameObject.transform.localRotation = shootDir.rotation;
                        magazine[i].Damage = BulletDamage;
                        magazine[i].gameObject.SetActive(true);
                        bulletFound = true;
                        break;
                    }
                }

                if (!bulletFound)
                {
                    Bullet_Shotgun bulletInstance = Instantiate(bullet, transform.position, shootDir.rotation);
                    bulletInstance.Damage = BulletDamage;
                    magazine.Add(bulletInstance);
                }
            }
        }
    }
}