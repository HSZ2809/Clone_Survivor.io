using UnityEngine;

namespace ZUN
{
    public abstract class Monster : MonoBehaviour
    {
        [Header("Status")]
        [SerializeField] protected float hp = 0.0f;
        [SerializeField] protected float attackPower = 0.0f;
        [SerializeField] protected float moveSpeed = 0.0f;
        protected float speed;

        protected ChapterCtrl chapterCtrl;
        protected Transform characterTF;

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
        }

        public float Hp
        {
            get { return hp; }
            set { hp = value; }
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