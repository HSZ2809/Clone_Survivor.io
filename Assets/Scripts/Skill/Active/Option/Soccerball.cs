using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Soccerball : ActiveSkill
    {
        [Header("Spac")]
        [SerializeField] private float coefficient;
        [SerializeField] private float cooldown;
        [SerializeField] private int magazineSize;
        [SerializeField] private float moveSpeed;

        [Header("Bullet")]
        [SerializeField] private Bullet_Soccerball Bullet = null;
        [SerializeField] private List<Bullet_Soccerball> magazine = null;

        public float BulletDamage { get { return coefficient * character.Atk; } }

        IEnumerator enumerator;

        private void Start()
        {
            enumerator = Shoot();
            character.SetActiveSkill(this);

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

                yield return new WaitForSeconds(cooldown * character.AtkSpeed);
            }
        }

        public override void Upgrade()
        {
            StopCoroutine(enumerator);

            level += 1;

            switch (level)
            {
                case 2:
                    magazineSize += 1;
                    break;
                case 3:
                    moveSpeed += 3;
                    coefficient = 8.0f;
                    break;
                case 4:
                    moveSpeed += 3;
                    coefficient = 10.0f;
                    break;
                case 5:
                    magazineSize += 1;
                    break;
                case 6:
                    Debug.Log("Soccerball TryUpgrade() : evolution");
                    break;
                default:
                    Debug.LogWarning("Soccerball TryUpgrade() : invalid level");
                    break;
            }

            StartCoroutine(enumerator);
        }
    }
}