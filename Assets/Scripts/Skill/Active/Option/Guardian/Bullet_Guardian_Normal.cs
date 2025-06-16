using DG.Tweening;
using UnityEngine;

namespace ZUN
{
    public class Bullet_Guardian_Normal : Bullet
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private CircleCollider2D cc2D;

        [SerializeField] private float rotationSpeed;

        private Sequence sizeupSequence;
        private Sequence sizedownSequence;

        private void Start()
        {
            sizeupSequence = DOTween.Sequence();
            sizeupSequence.Append(transform.DOScale(1.5f, 0.3f));
            sizeupSequence.Pause();
            sizeupSequence.SetAutoKill(false);

            sizedownSequence = DOTween.Sequence();
            sizedownSequence.Append(transform.DOScale(0.0f, 0.3f));
            sizedownSequence.Pause();
            sizedownSequence.SetAutoKill(false);
        }

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

        public void BulletEnable()
        {
            sizeupSequence.Restart();
            cc2D.enabled = true;
        }

        public void BulletDisable()
        {
            sizedownSequence.Restart();
            cc2D.enabled = false;
        }
    }
}