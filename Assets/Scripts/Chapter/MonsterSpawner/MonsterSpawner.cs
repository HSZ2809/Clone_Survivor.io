using UnityEngine;

namespace ZUN
{
    public abstract class MonsterSpawner : MonoBehaviour
    {
        #region Inspector
        [Header("Monster")]
        [SerializeField] protected Monster monster;
        [SerializeField] protected float hp;
        [SerializeField] protected float ap;

        [Header("Amount of monsters")]
        [SerializeField] protected int amount;
        #endregion

        protected Character character;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        }

        public abstract void SetAmount(int _amount);
    }
}