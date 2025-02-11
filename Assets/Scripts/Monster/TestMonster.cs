using UnityEngine;

namespace ZUN
{
    public class TestMonster : MonoBehaviour, IMon_Movement, IMon_Damageable, IMon_Attackable, IMon_Destroyable
    {
        #region Inspector
        [Header("Status")]
        [SerializeField] protected float hp;
        [SerializeField] protected float ap;
        [SerializeField] protected float moveSpeed;
        #endregion

        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float MaxHp { get; set; }
        public float Ap { get => ap; set => ap = value; }


        public void Move()
        {
            throw new System.NotImplementedException();
        }

        public void ShowDamage(float damage)
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }
        public void Die()
        {
            throw new System.NotImplementedException();
        }
    }
}