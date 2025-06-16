using UnityEngine;

namespace ZUN
{
    public class Bullet_Guardian_Final : Bullet
    {
        [SerializeField] private AudioSource audioSource;

        [SerializeField] private float rotationSpeed;

        private void Update()
        {
            sr[0].transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.CompareTag("Monster"))
            {
                if (coll.gameObject.TryGetComponent<IKnockBackable>(out var mon_KnockBackable))
                {
                    mon_KnockBackable.KnockBack();
                }

                if (coll.gameObject.TryGetComponent<IBleeding>(out var mon_Bleeding))
                {
                    mon_Bleeding.Bleeding();
                }

                if (coll.gameObject.TryGetComponent<IDamageable>(out var mon_Damageable))
                {
                    mon_Damageable.TryTakeDamage(damage);
                    audioSource.Play();
                }
            }

            if (coll.gameObject.TryGetComponent<IBlockable>(out var monsterBullet))
            {
                monsterBullet.Block();
            }
        }
    }
}