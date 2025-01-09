using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Shuriken : Weapon
    {
        [SerializeField] private SpriteRenderer handSprite = null;

        [SerializeField] private Transform shootDirection = null;

        [Header("Spac")]
        [SerializeField] private float defaultDamage = 0.0f;
        [SerializeField] private float reloadTime = 1.0f;

        [Header("Range")]
        LayerMask monsterLayer;
        [SerializeField] float findRange = 10.0f;

        [Header("Magazine")]
        [SerializeField] private List<Bullet> magazine = null;

        public float BulletDamage { get { return defaultDamage + character.AttackPower; } }

        IEnumerator enumerator;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            handSprite.sortingLayerName = "Weapon";
            monsterLayer = (1 << LayerMask.NameToLayer("Monster"));
        }

        public override void ActivateWeapon(float attackSpeed)
        {
            enumerator = Shoot(attackSpeed);
            StartCoroutine(enumerator);
        }

        IEnumerator Shoot(float attackSpeed)
        {
            while (true)
            {
                yield return new WaitForSeconds(reloadTime * attackSpeed);

                bool bulletFound = false;
                SetAim();

                for (int i = 0; i < magazine.Count; i++)
                {
                    if (!magazine[i].gameObject.activeSelf)
                    {
                        magazine[i].gameObject.transform.position = shootDirection.position;
                        magazine[i].gameObject.transform.localRotation = shootDirection.rotation;
                        magazine[i].Damage = BulletDamage;
                        magazine[i].gameObject.SetActive(true);
                        bulletFound = true;
                        break;
                    }
                }

                if (!bulletFound)
                {
                    Bullet bulletInstance = Instantiate(bullet, shootDirection.position, shootDirection.rotation);
                    bulletInstance.Damage = BulletDamage;
                    magazine.Add(bulletInstance);
                }
            }
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
                shootDirection.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, findRange);
        }
    }
}