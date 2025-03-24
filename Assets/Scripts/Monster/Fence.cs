using DG.Tweening;
using UnityEngine;

namespace ZUN
{
    public class Fence : MonoBehaviour, IDamageable, IAttackable, IDestroyable
    {
        #region Inspector
        [Header("Status")]
        [SerializeField] float ap;

        [Header("EXP Type")]
        [SerializeField] private EXPShard.Type shardType;
        #endregion

        SpriteRenderer sr;
        Character character;
        ObjectPool_ExpShard EXPPool;
        ObjectPool_DamageText damageTextPool;

        public float Ap { get => ap; set => ap = value; }

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            EXPPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ObjectPool_ExpShard>();
            damageTextPool = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ObjectPool_DamageText>();
            tag = "Fence";
            gameObject.layer = LayerMask.NameToLayer("Wall");
            sr = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            transform.parent = null;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Character"))
            {
                character.TakeDamage(ap);
            }
        }

        public float TakeDamage(float damage)
        {
            ShowDamage(damage);

            return 0;
        }

        public void ShowDamage(float damage)
        {
            DamageText damageText = damageTextPool.Pool.Get();
            damageText.transform.position = transform.position;
            damageText.SetText(damage.ToString());
        }

        public void Die()
        {
            EXPPool.SetShard(shardType, transform.position);

            Sequence fadeoutSequence = DOTween.Sequence();
            fadeoutSequence.Append(sr.DOFade(0.0f, 1.0f));
            fadeoutSequence.OnComplete(() =>
            {
                Destroy(gameObject);
            });

            fadeoutSequence.Play();
        }
    }
}