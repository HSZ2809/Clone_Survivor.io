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

        private void Awake()
        {
            tag = "Monster";
            gameObject.layer = LayerMask.NameToLayer(tag);
            cc2D = GetComponent<CircleCollider2D>();
        }

        public float AttackPower
        {
            get { return attackPower; }
        }

        public virtual void Hit(float damage)
        {
            Debug.LogWarning("Monster : Hit() is not Set");
        }
    }
}