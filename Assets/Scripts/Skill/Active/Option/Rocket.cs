using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Rocket : ActiveSkill
    {
        [Space]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip clip;
        [SerializeField] private Transform shootDir = null;

        [Header("Spac")]
        [SerializeField] private float coefficient;
        [SerializeField] private float cooldown;
        [SerializeField] private int magazineSize;
        [SerializeField] private float firerate;
        readonly float findRange = 10.0f;
        LayerMask monsterLayer;

        [Header("Bullet")]
        [SerializeField] private Bullet_Rocket prefab_bullet = null;
        [SerializeField] private List<Bullet_Rocket> objPool = null;

        IEnumerator enumerator;

        public float BulletDamage { get { return coefficient * character.Atk; } }

        private void Start()
        {
            monsterLayer = (1 << LayerMask.NameToLayer("Monster"));
            enumerator = Shoot();
            character.SetActiveSkill(this);
        }

        public override void ActiveSkillOn()
        {
            StartCoroutine(enumerator);
        }

        IEnumerator Shoot()
        {
            while (true)
            {
                for (int i = 0; i < magazineSize; i++)
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
                        Bullet_Rocket bulletInstance = Instantiate(prefab_bullet, shootDir.position, shootDir.rotation);
                        bulletInstance.Damage = BulletDamage;
                        objPool.Add(bulletInstance);
                    }

                    audioSource.PlayOneShot(clip);
                }

                yield return new WaitForSeconds(cooldown * character.AtkSpeed);
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
                shootDir.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
            }
        }

        public override void Upgrade()
        {
            StopCoroutine(enumerator);

            level += 1;

            switch (level)
            {
                case 2:
                    coefficient = 4;
                    break;
                case 3:
                    magazineSize += 1;
                    coefficient = 4.0f;
                    break;
                case 4:
                    coefficient = 6.0f;
                    break; ;
                case 5:
                    magazineSize += 1;
                    break;
                case 6:
                    coefficient = 20.0f;
                    Debug.Log("Rocket TryUpgrade() : evolution");
                    break;
                default:
                    Debug.LogWarning("Rocket TryUpgrade() : invalid level");
                    break;
            }

            StartCoroutine(enumerator);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, findRange);
        }
    }
}