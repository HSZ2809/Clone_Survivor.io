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
            sizedownSequence.OnStart(() =>
            {
                cc2D.enabled = false;
            });
            sizedownSequence.Append(transform.DOScale(0.0f, 0.3f));

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
                coll.gameObject.GetComponent<IMon_Damageable>().TakeDamage(damage);
                audioSource.Play();
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