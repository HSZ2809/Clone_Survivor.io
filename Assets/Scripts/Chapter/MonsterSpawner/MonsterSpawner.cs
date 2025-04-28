using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public abstract class MonsterSpawner : MonoBehaviour
    {
        #region Inspector
        [Header("Monster")]
        [SerializeField] protected NomalMonster monster;
        [SerializeField] protected float hp;
        [SerializeField] protected float ap;

        [Header("Amount of monsters")]
        [SerializeField] protected int amount;
        #endregion

        protected int currentAmount;
        protected Queue<NomalMonster> objPool = new Queue<NomalMonster>();

        public void Release(NomalMonster _monster)
        {
            objPool.Enqueue(_monster);
            currentAmount -= 1;
        }

        public abstract void SetAmount(int _amount);
    }
}