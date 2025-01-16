using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Brick : Weapon
    {
        [Header("Spac")]
        [SerializeField] private float damage = 0.0f;
        [SerializeField] private float cooldown = 1.0f;
        [SerializeField] private float firerate = 0.3f;
        [SerializeField] private int magazineSize = 1;

        [Header("Bullet")]
        [SerializeField] private Bullet_Brick prefab_bullet = null;
        [SerializeField] private List<Bullet_Brick> objPool = null;

        IEnumerator enumerator;

        public float BulletDamage { get { return damage + character.AttackPower; } }

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
            while(true)
            {
                for (int i = 0; i < magazineSize; i++)
                {
                    yield return new WaitForSeconds(firerate);

                    bool bulletFound = false;

                    for (int k = 0; k < objPool.Count; k++)
                    {
                        if (!objPool[i].gameObject.activeSelf)
                        {
                            objPool[i].gameObject.transform.position = transform.position;
                            objPool[i].Damage = BulletDamage;
                            objPool[i].gameObject.SetActive(true);
                            bulletFound = true;
                            break;
                        }
                    }

                    if (!bulletFound)
                    {
                        Bullet_Brick bulletInstance = Instantiate(prefab_bullet, transform.position, transform.rotation);
                        bulletInstance.Damage = BulletDamage;
                        objPool.Add(bulletInstance);
                    }
                }

                yield return new WaitForSeconds(cooldown * character.AttackSpeed);
            }
        }

        public override bool TryUpgrade(int level)
        {
            StopCoroutine(enumerator);

            switch(level)
            {
                case 2:
                    magazineSize += 1;
                    damage += 10;
                    break;
                case 3:
                    magazineSize += 1;
                    damage += 10;
                    break;
                case 4:
                    magazineSize += 1;
                    damage += 10;
                    break;
                case 5:
                    magazineSize += 1;
                    damage += 10;
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