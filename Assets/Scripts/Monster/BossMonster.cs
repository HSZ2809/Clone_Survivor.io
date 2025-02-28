using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public abstract class BossMonster : Monster
    {
        [SerializeField] protected string bossName;
        protected GameObject bossHpUI;
        protected Image bossHpBar;

        public string GetBossName()
        {
            return bossName;
        }

        public void SetBossHpUI(GameObject ui)
        {
            bossHpUI = ui;
        }

        public void SetBossHpBar(Image hpBar)
        {
            bossHpBar = hpBar;
        }
    }
}