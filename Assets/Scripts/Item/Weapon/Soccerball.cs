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

        [Header("Bullet")]
        [SerializeField] private Bullet_Soccerball Bullet = null;
        [SerializeField] private List<Bullet_Soccerball> magazine = null;

        public float BulletDamage { get { return damage + character.AttackPower; } }

        IEnumerator enumerator;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            enumerator = Shoot();
        }

        public override void ActivateWeapon()
        {
            StartCoroutine(enumerator);
        }

        IEnumerator Shoot()
        {
            while (true)
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
                            magazine[k].CharMoveSpeed = character.MoveSpeed;
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
                        bulletInstance.CharMoveSpeed = character.MoveSpeed;
                        magazine.Add(bulletInstance);
                    }
                }

                yield return new WaitForSeconds(cooldown * character.AttackSpeed);
            }
        }

        public override bool TryUpgrade(int level)
        {
            StopCoroutine(enumerator);

            switch (level)
            {
                case 2:
                    magazineSize += 1;
                    break;
                case 3:
                    moveSpeed += 3;
                    damage += 10;
                    break;
                case 4:
                    moveSpeed += 3;
                    damage += 10;
                    break;
                case 5:
                    magazineSize += 1;
                    break;
                case 6:
                    Debug.Log("Brick TryUpgrade() : evolution");
                    break;
                default:
                    Debug.LogWarning("Brick TryUpgrade() : invalid level");
                    return false;
            }

            enumerator = Shoot();
            StartCoroutine(enumerator);

            return true;
        }
    }
}