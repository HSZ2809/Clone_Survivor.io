using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Shotgun : ActiveSkill
    {
        [SerializeField] private Transform shootDir = null;

        [SerializeField] private float coefficient;
        [SerializeField] private float cooldown;
        [SerializeField] private int magazineSize = 1;

        [Header("Bullet")]
        [SerializeField] private Bullet_Shotgun prefab_bullet = null;
        [SerializeField] private List<Bullet_Shotgun> objPool = null;

        public float BulletDamage { get { return coefficient * character.Atk; } }
        public float Cooldown { get { return cooldown * character.AtkSpeed; } }

        IEnumerator enumerator;

        private void Awake() 
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            shootDir = character.GetShootDir();
            enumerator = Shoot();
        }

        public override void ActiveSkillOn()
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
                
                yield return new WaitForSeconds(Cooldown);
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

        public override void Upgrade()
        {
            level += 1;

            switch (level)
            {
                case 2:
                    magazineSize += 3;
                    coefficient = 2;
                    break;
                case 3:
                    magazineSize += 3;
                    coefficient = 3;
                    break;
                case 4:
                    magazineSize += 3;
                    coefficient = 4;
                    break;
                case 5:
                    magazineSize += 3;
                    coefficient = 5;
                    break;
                case 6:
                    coefficient = 5;
                    Debug.Log("Shotgun TryUpgrade() : evolution");
                    break;
                default:
                    Debug.LogWarning("Shotgun TryUpgrade() : invalid level");
                    break;
            }

            SetMag();
        }
    }
}