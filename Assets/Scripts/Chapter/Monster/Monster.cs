using UnityEngine;

namespace ZUN
{
    public abstract class Monster : MonoBehaviour
    {
        [Header("Status")]
        [SerializeField] protected float maxHp;
        [SerializeField] protected float attackPower;
        [SerializeField] protected float moveSpeed;
        protected float hp;

        protected ChapterCtrl chapterCtrl;
        protected Transform characterTF;

        protected CircleCollider2D cc2D;

        public ChapterCtrl _chapterCtrl 
        { 
            set 
            {
                if (chapterCtrl == null)
                    chapterCtrl = value; 
            } 
        }

        public Transform CharacterTF
        {
            set
            {
                if (characterTF == null)
                    characterTF = value;
            }
        }

        public float AttackPower
        {
            get { return attackPower; }
        }

        public float MaxHp { get { return maxHp; } set { maxHp = value; } }

        public abstract void Hit(float damage);
    }
}