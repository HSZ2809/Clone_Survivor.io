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

        protected MissionCtrl missionCtrl;
        protected Transform characterTF;

        public MissionCtrl _MissionCtrl 
        { 
            set 
            {
                if (missionCtrl == null)
                    missionCtrl = value; 
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