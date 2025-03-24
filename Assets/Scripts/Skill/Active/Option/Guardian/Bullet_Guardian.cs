using DG.Tweening;
using UnityEngine;

namespace ZUN
{
    public class Bullet_Guardian : Bullet
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private CircleCollider2D cc2D;

        [SerializeField] private float rotationSpeed;

        private Sequence sizeupSequence;
        private Sequence sizedownSequence;

        private void Start()
        {
            sizeupSequence = DOTween.Sequence();
            sizeupSequence.Append(transform.DOScale(1.5f, 0.3f));
            sizeupSequence.OnComplete(() =>
            {
                cc2D.enabled = true;
            });

            sizeupSequence.Pause();
            sizeupSequence.SetAutoKill(false);

            sizedownSequence = DOTween.Sequence();
            sizedownSequence.Append(transform.DOScale(0.0f, 0.3f));
            sizedownSequence.OnStart(() =>
            {
                cc2D.enabled = false;
            });

            sizedownSequence.Pause();
            sizedownSequence.SetAutoKill(false);
        }

        private void Update()
        {
            sr.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.CompareTag("Monster"))
            {
                IKnockBackable mon_knockBack = coll.gameObject.GetComponent<IKnockBackable>();
                if (mon_knockBack != null)
                {
                    mon_knockBack.KnockBack();
                }

                IBleeding mon_bleeding = coll.gameObject.GetComponent<IBleeding>();
                if (mon_bleeding != null)
                {
                    mon_bleeding.Bleeding();
                }

                IDamageable mon_damage = coll.gameObject.GetComponent<IDamageable>();
                if (mon_damage != null)
                {
                    mon_damage.TakeDamage(damage);
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
        }

        public void BulletDisable()
        {
            sizedownSequence.Restart();
        }
    }
}