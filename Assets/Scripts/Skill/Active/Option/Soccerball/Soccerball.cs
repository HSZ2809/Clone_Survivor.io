using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

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
        [SerializeField] private Bullet_Soccerball bulletPrefab = null;

        IObjectPool<Bullet_Soccerball> objPool;
        IEnumerator enumerator;

        public float BulletDamage { get { return coefficient * character.Atk; } }

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            manager_Audio = GameObject.FindObjectWithTag("Manager").GetComponent<Manager_Audio>();
            level = 1;
            objPool = new ObjectPool<Bullet_Soccerball>(CreateBullet, null, OnReleaseBullet, OnDestroyBullet, maxSize: 5);
            enumerator = Shoot();
            character.SetActiveSkill(this);
        }

        private void OnEnable()
        {
            StartCoroutine(enumerator);
        }

        IEnumerator Shoot()
        {
            while (true)
            {
                for (int i = 0; i < magazineSize; i++)
                {
                    Bullet_Soccerball bullet = objPool.Get();
                    bullet.gameObject.transform.position = transform.position;
                    bullet.Damage = BulletDamage;
                    bullet.MoveSpeed = moveSpeed;
                    bullet.CharMoveSpeed = character.MoveSpeed;
                    bullet.gameObject.SetActive(true);
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

        Bullet_Soccerball CreateBullet()
        {
            Bullet_Soccerball bullet = Instantiate(bulletPrefab);
            bullet.SetBulletPool(objPool);
            return bullet;
        }

        void OnReleaseBullet(Bullet_Soccerball bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        void OnDestroyBullet(Bullet_Soccerball bullet)
        {
            Destroy(bullet.gameObject);
        }
    }
}
