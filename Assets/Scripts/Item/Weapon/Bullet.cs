using UnityEngine;

namespace ZUN
{
    public abstract class Bullet : MonoBehaviour
    {
        [SerializeField] protected float damage = 1.0f;

        private void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Bullet");
        }

        public float Damage
        {
            set { damage = value; }
        }
    }
}