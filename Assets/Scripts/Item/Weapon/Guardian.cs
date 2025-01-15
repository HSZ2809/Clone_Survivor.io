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
        [SerializeField] float damage;
        [SerializeField] float cooldown;
        [SerializeField] float rotationSpeed;
        [SerializeField] float range;
        [SerializeField] float duration;
        [SerializeField] int magazineSize;

        [Header("Bullet")]
        [SerializeField] private Bullet_Guardian prefab_bullet = null;
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
            SetAngle();
        }

        IEnumerator LoadWeapon()
        {
            yield return new WaitForSeconds(cooldown * character.AttackSpeed);

            StartCoroutine(Shoot());
        }

        IEnumerator Shoot()
        {
            foreach (var obj in objPool)
                obj.gameObject.SetActive(true);

            yield return new WaitForSeconds(duration);

            foreach (var obj in objPool)
                obj.gameObject.SetActive(false);

            StartCoroutine(LoadWeapon());
        }

        void SetAngle()
        {
            StopCoroutine(LoadWeapon());
            StopCoroutine(Shoot());

            foreach(var obj in objPool)
                obj.gameObject.SetActive(false);

            while(magazineSize > objPool.Count)
            {
                Bullet_Guardian bulletInstance = Instantiate(prefab_bullet, transform.position, transform.rotation);
                bulletInstance.transform.parent = pivot.transform;
                objPool.Add(bulletInstance);
            }

            float angle = (360f / magazineSize) * Mathf.Deg2Rad;

            for (int i = 0; i < objPool.Count; i++)
            {
                float x = Mathf.Cos(angle * i);
                float y = Mathf.Sin(angle * i);
                Vector3 temp = new(transform.position.x + (x * range), transform.position.y + (y * range), 0);

                objPool[i].gameObject.transform.position = temp;
                objPool[i].Damage = BulletDamage;
            }

            StartCoroutine(Shoot());
        }

        public override bool TryUpgrade(int level)
        {
            switch(level)
            {
                case 2:
                    magazineSize += 1;
                    damage += 10;
                    SetAngle();
                    return true;
                case 3:
                    magazineSize += 1;
                    damage += 10;
                    SetAngle();
                    return true;
                case 4:
                    magazineSize += 1;
                    damage += 10;
                    SetAngle();
                    return true;
                case 5:
                    magazineSize += 1;
                    damage += 10;
                    SetAngle();
                    return true;
                case 6:
                    Debug.Log("Guardian TryUpgrade() : evolution");
                    return true;
                default:
                    Debug.LogWarning("Guardian TryUpgrade() : invalid level");
                    return false;
            }
        }
    }
}