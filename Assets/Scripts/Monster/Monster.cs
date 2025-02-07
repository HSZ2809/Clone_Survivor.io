using UnityEngine;
using UnityEngine.TextCore.Text;

namespace ZUN
{
    public abstract class Monster : MonoBehaviour
    {
        #region Inspector
        [Header("Status")]
        [SerializeField] protected float hp;
        [SerializeField] protected float attackPower;
        #endregion

        protected ChapterCtrl chapterCtrl;
        protected Character character;

        protected CircleCollider2D cc2D;

        public abstract void Hit(float damage);
        public void SetMonsterSpec(float _hp,  float _attackPower)
        {
            hp = _hp;
            attackPower = _attackPower;
        }
    }
}