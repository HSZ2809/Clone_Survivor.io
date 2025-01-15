using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ZUN
{
    public class Shuriken : Weapon
    {
        [SerializeField] private SpriteRenderer handSprite = null;
        [SerializeField] private Transform shootDir = null;

        [Header("Spac")]
        [SerializeField] private float damage;
        [SerializeField] private float cooldown;
        [SerializeField] private int magazineSize;
        [SerializeField] private float firerate;
        readonly float findRange = 10.0f;
        LayerMask monsterLayer;

        [Header("Magazine")]
        [SerializeField] private Bullet_Shuriken prefab_bullet = null;
        [SerializeField] private List<Bullet_Shuriken> objPool = null;

        public float BulletDamage { get { return damage + character.AttackPower; } }

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            handSprite.sortingLayerName = "Weapon";
            monsterLayer = (1 << LayerMask.NameToLayer("Monster"));
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
                SetAim();

                for (int k = 0; k < objPool.Count; k++)
                {
                    if (!objPool[k].gameObject.activeSelf)
                    {
                        objPool[k].gameObject.transform.position = shootDir.position;
                        objPool[k].gameObject.transform.localRotation = shootDir.rotation;
                        objPool[k].Damage = BulletDamage;
                        objPool[k].gameObject.SetActive(true);
                        bulletFound = true;
                        break;
                    }
                }

                if (!bulletFound)
                {
                    Bullet_Shuriken bulletInstance = Instantiate(prefab_bullet, shootDir.position, shootDir.rotation);
                    bulletInstance.Damage = BulletDamage;
                    objPool.Add(bulletInstance);
                }
            }

            StartCoroutine(LoadWeapon());
        }

        private void SetAim()
        {
            Vector3 aim;

            Collider2D[] monsterCol;
            monsterCol = Physics2D.OverlapCircleAll(transform.position, findRange, monsterLayer);

            if (monsterCol.Length < 1)
                monsterCol = Physics2D.OverlapCircleAll(transform.position, findRange * 2, monsterLayer);

            if (monsterCol.Length < 1)
                monsterCol = Physics2D.OverlapCircleAll(transform.position, findRange * 3, monsterLayer);

            if (monsterCol.Length > 0)
            {
                Transform nearestMon = monsterCol[0].transform;
                float distance = Vector3.Distance(transform.position, nearestMon.position);

                foreach (Collider2D col in monsterCol)
                {
                    float compareDis = Vector3.Distance(transform.position, col.transform.position);

                    if (compareDis < distance)
                    {
                        nearestMon = col.transform;
                        distance = compareDis;
                    }
                }

                aim = (transform.position - nearestMon.position).normalized;
                float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
                shootDir.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            }
        }

        public override bool TryUpgrade(int level)
        {
            switch (level)
            {
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, findRange);
        }
    }
}