using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Soccerball : Weapon
    {
        [Header("Spac")]
        [SerializeField] private float damage;
        [SerializeField] private float cooldown;
        [SerializeField] private int magazineSize;
        [SerializeField] private float moveSpeed;

        [Header("Magazine")]
        [SerializeField] private Bullet_Soccerball Bullet = null;
        [SerializeField] private List<Bullet_Soccerball> magazine = null;

        public float BulletDamage { get { return damage + character.AttackPower; } }

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        }

        public override void ActivateWeapon()
        {
            StartCoroutine(Shoot());
        }

        IEnumerator LoadWeapon()
        {
            yield return new WaitForSeconds(cooldown * character.AttackSpeed);

            StartCoroutine(Shoot());
        }

        IEnumerator Shoot()
        {
            for (int i = 0; i < magazineSize; i++)
            {
                bool bulletFound = false;

                for (int k = 0; k < magazine.Count; k++)
                {
                    if (!magazine[k].gameObject.activeSelf)
                    {
                        magazine[k].gameObject.transform.position = transform.position;
                        magazine[k].Damage = BulletDamage;
                        magazine[k].MoveSpeed = moveSpeed;
                        magazine[k].gameObject.SetActive(true);
                        bulletFound = true;
                        break;
                    }
                }

                if (!bulletFound)
                {
                    Bullet_Soccerball bulletInstance = Instantiate(Bullet, transform.position, transform.rotation);
                    bulletInstance.Damage = BulletDamage;
                    bulletInstance.MoveSpeed = moveSpeed;
                    magazine.Add(bulletInstance);
                }
            }

            StartCoroutine(LoadWeapon());
            yield return null;
        }

        public override bool TryUpgrade(int level)
        {
            switch (level)
            {
                case 2:
                    magazineSize += 1;
                    return true;
                case 3:
                    moveSpeed += 3;
                    damage += 10;
                    return true;
                case 4:
                    moveSpeed += 3;
                    damage += 10;
                    return true;
                case 5:
                    magazineSize += 1;
                    return true;
                case 6:
                    Debug.Log("Brick TryUpgrade() : evolution");
                    return true;
                default:
                    Debug.LogWarning("Brick TryUpgrade() : invalid level");
                    return false;
            }
        }

        public void TempMethod()
        {
            TryUpgrade(2);
        }
    }
}