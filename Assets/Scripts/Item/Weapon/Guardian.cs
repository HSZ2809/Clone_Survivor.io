using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ZUN
{
    public class Guardian : Weapon
    {
        [SerializeField] private Transform pivot = null;

        [Header("Spac")]
        [SerializeField] float damage = 0.0f;
        [SerializeField] float cooldown = 1.0f;
        [SerializeField] float rotationSpeed = 1.0f;
        [SerializeField] float range = 1.0f;
        [SerializeField] int magazineSize = 2;

        [Header("Magazine")]
        [SerializeField] private Bullet_Guardian bullet = null;
        [SerializeField] private List<Bullet_Guardian> objPool = null;

        public float BulletDamage { get { return damage + character.AttackPower; } }

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
            

            StartCoroutine(LoadWeapon());
        }
        IEnumerator LoadWeapon()
        {
            yield return new WaitForSeconds(cooldown * character.AttackSpeed);

            // StartCoroutine(Shoot());
        }

        //IEnumerator Shoot()
        //{
        //    float angle = (360f / magazineSize) * Mathf.Deg2Rad;

        //    for (int i = 0; i < magazineSize; i++)
        //    {
        //        float x = Mathf.Cos(angle * i);
        //        float y = Mathf.Sin(angle * i);
        //        Vector3 temp = new(x * range, y * range, 0);

                

        //        Bullet_Guardian bulletInstance = Instantiate(bullet, temp, transform.rotation);
        //        bulletInstance.transform.parent = pivot.transform;
        //        bulletInstance.Damage = BulletDamage;
        //        objPool.Add(bulletInstance);
        //    }
        //}
    }
}