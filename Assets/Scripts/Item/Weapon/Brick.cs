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

        [Header("bullet")]
        [SerializeField] private Bullet_Brick bullet = null;
        [SerializeField] private List<Bullet_Brick> objPool = null;

        public float BulletDamage { get { return damage + character.AttackPower; } }

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        }

        public override void ActivateWeapon()
        {
            StartCoroutine(LoadWeapon());
        }

        IEnumerator LoadWeapon()
        {
            yield return new WaitForSeconds(cooldown * character.AttackSpeed);

            StartCoroutine(Shoot());
        }

        IEnumerator Shoot()
        {
            for(int i = 0; i < magazineSize; i++)
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
                    Bullet_Brick bulletInstance = Instantiate(bullet, transform.position, transform.rotation);
                    bulletInstance.Damage = BulletDamage;
                    objPool.Add(bulletInstance);
                }
            }

            StartCoroutine(LoadWeapon());
        }

        public override bool TryUpgrade(int level)
        {
            /********************************
             * 
             * 1. 업그레이드할 레벨을 받아온다
             * 2. switch문으로 해당 효과를 적용한다
             * 3. 성공하면 성공 확인, 실패하면 실패 로직
             * 
             * @ case를 enum화 한다?
             ********************************/

            switch(level)
            {
                case 1:
                    magazineSize += 1;
                    damage += 10;
                    return true;
                case 2:
                    magazineSize += 1;
                    damage += 10;
                    return true;
                case 3:
                    magazineSize += 1;
                    damage += 10;
                    return true;
                case 4:
                    magazineSize += 1;
                    damage += 10;
                    return true;
                case 5:
                    magazineSize += 1;
                    damage += 10;
                    return true;
                case 6:
                    Debug.Log("Brick TryUpgrade() : evolution");
                    return true;
                default:
                    Debug.LogWarning("Brick TryUpgrade() : invalid level");
                    return false;
            }
        }
    }
}