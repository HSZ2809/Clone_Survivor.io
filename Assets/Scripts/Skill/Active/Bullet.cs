using UnityEngine;

namespace ZUN
{
    public abstract class Bullet : MonoBehaviour
    {
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
    }
}