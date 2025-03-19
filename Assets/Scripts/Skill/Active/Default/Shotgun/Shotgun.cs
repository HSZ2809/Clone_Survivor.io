using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class Shotgun : ActiveSkill
    {
        [Space]
        // [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip clip;
        [SerializeField] private Transform shootDir = null;

        [SerializeField] private float coefficient;
        [SerializeField] private float cooldown;
        [SerializeField] private int magazineSize = 3;

        [Header("Bullet")]
        [SerializeField] private Bullet_Shotgun bulletPrefab = null;
        IObjectPool<Bullet_Shotgun> objPool;

        public float BulletDamage { get { return coefficient * character.Atk; } }
        public float Cooldown { get { return cooldown * character.AtkSpeed; } }

        IEnumerator enumerator;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            manager_Audio = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_Audio>();
            level = 1;
            objPool = new ObjectPool<Bullet_Shotgun>(CreateBullet, null, OnReleaseBullet, OnDestroyBullet, maxSize: 15);
            shootDir = character.GetMoveDir();
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
                int Angle = magazineSize / 2;

                manager_Audio.SoundEffectPlayer.PlayOneShot(clip);

                for (int i = 0; i < magazineSize; i++)
                {
                    Bullet_Shotgun bullet = objPool.Get();
                    bullet.gameObject.transform.position = transform.position;
                    bullet.gameObject.transform.localRotation = shootDir.rotation;
                    bullet.gameObject.transform.Rotate(new Vector3(0, 0, 4 * (Angle - i)));
                    bullet.Damage = BulletDamage;
                    bullet.gameObject.SetActive(true);
                }

                yield return new WaitForSeconds(Cooldown);
            }
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
        }

        Bullet_Shotgun CreateBullet()
        {
            Bullet_Shotgun bullet = Instantiate(bulletPrefab);
            bullet.SetBulletPool(objPool);
            return bullet;
        }

        void OnReleaseBullet(Bullet_Shotgun bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        void OnDestroyBullet(Bullet_Shotgun bullet)
        {
            Destroy(bullet.gameObject);
        }
    }
}
