using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class Shuriken : ActiveSkill
    {
        [Space]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip clip;
        [SerializeField] private SpriteRenderer handSprite = null;
        [SerializeField] private Transform shootDir = null;
        [SerializeField] private Image reloadBar;

        [Header("Spac")]
        [SerializeField] private float coefficient;
        [SerializeField] private float cooldown;
        [SerializeField] private int magazineSize;
        readonly float findRange = 10.0f;
        LayerMask monsterLayer;

        [Header("Bullet")]
        [SerializeField] private Bullet_Shuriken prefab_bullet = null;
        [SerializeField] private List<Bullet_Shuriken> objPool = null;

        public float BulletDamage { get { return coefficient * character.Atk; } }
        public float Cooldown { get { return cooldown * character.AtkSpeed; } }

        IEnumerator enumerator;
        readonly WaitForFixedUpdate waitForFixedUpdate = new();
        readonly WaitForSeconds firerate = new(0.1f);

        private void Start()
        {
            handSprite.sortingLayerName = "Weapon";
            monsterLayer = (1 << LayerMask.NameToLayer("Monster"));
            reloadBar = character.ReloadBar();
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
                    yield return firerate;

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

                    audioSource.PlayOneShot(clip);
                }

                for(float waitTime = 0.0f; waitTime < Cooldown; waitTime += Time.deltaTime)
                {
                    yield return waitForFixedUpdate;
                    reloadBar.fillAmount = waitTime / Cooldown;
                }
                reloadBar.fillAmount = 0.0f;
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
                    magazineSize = 2;
                    coefficient = 2;
                    break;
                case 3:
                    magazineSize = 3;
                    coefficient = 3;
                    break;
                case 4:
                    magazineSize = 4;
                    coefficient = 4;
                    break; ;
                case 5:
                    magazineSize = 5;
                    coefficient = 5.6f;
                    break;
                case 6:
                    coefficient = 5.6f;
                    Debug.Log("Shuriken TryUpgrade() : evolution");
                    break;
                default:
                    Debug.LogWarning("Shuriken TryUpgrade() : invalid level");
                    break;
            }

            StartCoroutine(enumerator);
        }
    }
}