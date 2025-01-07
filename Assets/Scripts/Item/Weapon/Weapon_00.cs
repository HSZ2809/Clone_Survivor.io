using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Weapon_00 : Weapon
    {
        [SerializeField] protected Transform muzzle = null;
        [SerializeField] protected Bullet_00 bullet = null;

        [SerializeField] protected float defaultDamage = 0.0f;
        [SerializeField] protected float reloadTime = 1.0f;

        [Header("Magazine")]
        [SerializeField] protected List<Bullet_00> magazine = null;

        public float BulletDamage { get{ return defaultDamage + character.AttackPower; } }
        
        IEnumerator enumerator;

        private void Awake() 
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            muzzle = character.transform.Find("Shoot_Direction").GetChild(0);
        }

        public override void ActivateWeapon(float attackSpeed)
        {
            enumerator = Shoot(attackSpeed);
            StartCoroutine(enumerator);
        }

        IEnumerator Shoot(float attackSpeed)
        {
            Bullet_00 firstInstance = Instantiate(bullet, muzzle.position, muzzle.rotation);
            firstInstance.Damage = BulletDamage;
            magazine.Add(firstInstance);

            while(true)
            {
                yield return new WaitForSeconds(reloadTime * attackSpeed);

                for(int i = 0; i < magazine.Count; i++)
                {
                    if(magazine[i].gameObject.activeSelf == false)
                    {
                        magazine[i].gameObject.transform.position = muzzle.position;
                        magazine[i].gameObject.transform.localRotation = muzzle.rotation;
                        magazine[i].Damage = BulletDamage;
                        magazine[i].gameObject.SetActive(true);
                        break;
                    }    
                    
                    if(i == magazine.Count - 1)
                    {
                        Bullet_00 bulletInstance = Instantiate(bullet, muzzle.position, muzzle.rotation);
                        bulletInstance.Damage = BulletDamage;
                        magazine.Add(bulletInstance);
                        break;
                    }
                }
            }
        }
    }
}