using UnityEngine;

namespace ZUN
{
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] sr;
        [SerializeField] protected float damage = 1.0f;

        private void Awake()
        {
            tag = "Bullet";
            gameObject.layer = LayerMask.NameToLayer(tag);
        }

        public float Damage
        {
            set { damage = value; }
        }

        virtual public void InitializeSpriteAlpha(bool isEffectReduced)
        {
            if (isEffectReduced)
            {
                foreach (var sprite in sr)
                {
                    Color color = sprite.color;
                    color.a = 0.6f;
                    sprite.color = color;
                }
            }

        }
    }
}