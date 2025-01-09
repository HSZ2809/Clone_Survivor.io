using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Soccerball : Weapon
    {
        [SerializeField] private Transform shootDirection = null;

        [Header("Spac")]
        [SerializeField] private float defaultDamage = 0.0f;
        [SerializeField] private float reloadTime = 1.0f;

        LayerMask monsterLayer;

        [Header("Magazine")]
        [SerializeField] private List<Bullet> magazine = null;

        public float BulletDamage { get { return defaultDamage + character.AttackPower; } }

        IEnumerator enumerator;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            monsterLayer = (1 << LayerMask.NameToLayer("Monster"));
        }

        public override void ActivateWeapon(float attackSpeed)
        {
            enumerator = Shoot(attackSpeed);
            StartCoroutine(enumerator);
        }

        IEnumerator Shoot(float attackSpeed)
        {
            while (true)
            {
                yield return new WaitForSeconds(reloadTime * attackSpeed);

                bool bulletFound = false;
                shootDirection.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

                for (int i = 0; i < magazine.Count; i++)
                {
                    if (!magazine[i].gameObject.activeSelf)
                    {
                        magazine[i].gameObject.transform.position = shootDirection.position;
                        magazine[i].gameObject.transform.localRotation = shootDirection.rotation;
                        magazine[i].Damage = BulletDamage;
                        magazine[i].gameObject.SetActive(true);
                        bulletFound = true;
                        break;
                    }
                }

                if (!bulletFound)
                {
                    Bullet bulletInstance = Instantiate(bullet, shootDirection.position, shootDirection.rotation);
                    bulletInstance.Damage = BulletDamage;
                    magazine.Add(bulletInstance);
                }
            }
        }
    }
}