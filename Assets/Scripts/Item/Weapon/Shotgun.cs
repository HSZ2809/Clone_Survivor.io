using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***********************************************
 * 
 * 1. 캐릭터의 moveDir을 대체하는 조준선 생성
 * 2. 메거진 수에 따라서 총알 위치 조정, 생성
 * 
 ***********************************************/

namespace ZUN
{
    public class Shotgun : Weapon
    {
        [SerializeField] private Transform shootDir = null;

        [SerializeField] private float damage;
        [SerializeField] private float cooldown;
        [SerializeField] private int magazineSize = 1;

        [Header("Bullet")]
        [SerializeField] private Bullet_Shotgun prefab_bullet = null;
        [SerializeField] private List<Bullet_Shotgun> objPool = null;

        public float BulletDamage { get{ return damage + character.AttackPower; } }

        IEnumerator enumerator;

        private void Awake() 
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            shootDir = character.GetShootDir();
            enumerator = Shoot();
        }

        public override void ActivateWeapon()
        {
            SetMag();
        }

        IEnumerator Shoot()
        {
            int Angle = magazineSize / 2;

            while (true)
            {
                for (int i = 0; i < objPool.Count; i++)
                {
                    objPool[i].gameObject.transform.position = transform.position;
                    objPool[i].gameObject.transform.localRotation = shootDir.rotation;
                    objPool[i].gameObject.transform.Rotate(new Vector3(0, 0, 4 * (Angle - i)));
                    objPool[i].Damage = BulletDamage;
                    objPool[i].gameObject.SetActive(true);
                }
                
                yield return new WaitForSeconds(cooldown * character.AttackSpeed);
            }
        }

        void SetMag()
        {
            StopCoroutine(enumerator);

            foreach (var obj in objPool)
                obj.gameObject.SetActive(false);

            while (magazineSize > objPool.Count)
            {
                Bullet_Shotgun bulletInstance = Instantiate(prefab_bullet, transform.position, transform.rotation);
                objPool.Add(bulletInstance);
            }

            enumerator = Shoot();
            StartCoroutine(enumerator);
        }

        public override bool TryUpgrade(int level)
        {
            switch (level)
            {
                case 2:
                    magazineSize += 3;
                    damage += 10;
                    break;
                case 3:
                    magazineSize += 3;
                    damage += 10;
                    break;
                case 4:
                    magazineSize += 3;
                    damage += 10;
                    break;
                case 5:
                    magazineSize += 3;
                    damage += 10;
                    break;
                case 6:
                    Debug.Log("Shotgun TryUpgrade() : evolution");
                    break;
                default:
                    Debug.LogWarning("Shotgun TryUpgrade() : invalid level");
                    return false;
            }

            SetMag();
            return true;
        }
    }
}