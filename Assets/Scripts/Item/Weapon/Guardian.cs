using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Guardian : Weapon
    {
        [SerializeField] private Transform pivot = null;

        [Header("Spac")]
        [SerializeField] private float defaultDamage = 0.0f;
        [SerializeField] private float reloadTime = 1.0f;
        [SerializeField] private float rotationSpeed = 1.0f;
        [SerializeField] private float range = 1.0f;
        [SerializeField] int initMag = 2;

        [Header("Magazine")]
        [SerializeField] private Bullet_Guardian bullet = null;
        [SerializeField] private List<Bullet_Guardian> magazine = null;

        public float BulletDamage { get { return defaultDamage + character.AttackPower; } }

        IEnumerator enumerator;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        }
        private void Update()
        {
            pivot.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        public override void ActivateWeapon()
        {
            enumerator = Shoot();

            float angle = (360f / initMag) * Mathf.Deg2Rad;

            for (int i = 0; i < initMag; i++)
            {
                float x = Mathf.Cos(angle * i);
                float y = Mathf.Sin(angle * i);
                Vector3 temp = new(x * range, y * range, 0);

                Bullet_Guardian bulletInstance = Instantiate(bullet, temp, transform.rotation);
                bulletInstance.transform.parent = pivot.transform;
                bulletInstance.Damage = BulletDamage;
                magazine.Add(bulletInstance);
            }

            StartCoroutine(enumerator);
        }

        IEnumerator Shoot()
        {
            while (true)
            {
                yield return new WaitForSeconds(reloadTime * character.AttackSpeed);

                for (int i = 0; i < magazine.Count; i++)
                {
                    if (!magazine[i].gameObject.activeSelf)
                    {
                        magazine[i].Damage = BulletDamage;
                        magazine[i].gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}