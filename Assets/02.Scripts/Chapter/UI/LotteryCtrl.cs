using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class LotteryCtrl : MonoBehaviour
    {
        [SerializeField] private Image pnl_lottery;
        [SerializeField] private LotteryResult[] lotteryResults;
        [SerializeField] private Button closeLottery;
        [SerializeField] private TreasureBoxPopup treasureBoxPopup;

        public void StartLottery(ref ActiveSkill[] charActives, ref PassiveSkill[] charPassives)
        {
            Time.timeScale = 0;

            List<Skill> shuffleBox = new();

            for (int i = 0; i < charActives.Length; i++)
            {
                if (charActives[i] == null)
                    break;

                if (charActives[i].Level < charActives[i].SkillInfo.MaxLevel - 1)
                    shuffleBox.Add(charActives[i]);
                else if (charActives[i].Level == charActives[i].SkillInfo.MaxLevel - 1)
                {
                    string synergyID = charActives[i].SynergyID;

                    if (Array.FindIndex(charPassives, element => element != null && element.SkillInfo.ID == synergyID) > -1)
                        shuffleBox.Add(charActives[i]);
                }
            }

            for (int i = 0; i < charPassives.Length; i++)
            {
                if (charPassives[i] == null)
                    break;

                if (charPassives[i].Level < charPassives[i].SkillInfo.MaxLevel)
                    shuffleBox.Add(charPassives[i]);
            }

            int randomInt = UnityEngine.Random.Range(0, 100);
            int amount = randomInt <= 2 ? 5 : randomInt <= 20 ? 3 : 1;
            amount = 3;

            List<Skill> winners      = new();
            List<int>   winnerLevels = new();
            for (int i = 0; i < amount; i++)
            {
                if (shuffleBox.Count < 1)
                    continue;

                Skill skill = GetRandomSkill(ref shuffleBox);
                skill.Upgrade();
                int displayLevel = skill.Level;
                lotteryResults[i].SetData(skill, displayLevel);
                winnerLevels.Add(displayLevel);

                // ActiveSkill은 MaxLevel-1 도달 시 이번 추첨에서 제외 — 진화는 다음 추첨에서만 가능
                // PassiveSkill은 MaxLevel 도달 시에만 제외
                bool removeThreshold = skill is ActiveSkill
                    ? skill.Level >= skill.SkillInfo.MaxLevel - 1
                    : skill.Level >= skill.SkillInfo.MaxLevel;
                if (removeThreshold)
                    shuffleBox.Remove(skill);

                winners.Add(skill);
            }

            if (winners.Count == 0 || treasureBoxPopup == null)
            {
                for (int i = 0; i < winners.Count; i++)
                    lotteryResults[i].gameObject.SetActive(true);

                closeLottery.gameObject.SetActive(true);
                pnl_lottery.gameObject.SetActive(true);
                return;
            }

            Sprite[] candidateSprites = new Sprite[shuffleBox.Count];
            for (int i = 0; i < shuffleBox.Count; i++)
                candidateSprites[i] = shuffleBox[i].SkillInfo.Sprite[0];

            treasureBoxPopup.Open(winners.ToArray(), winnerLevels.ToArray(), candidateSprites, () =>
            {
                Time.timeScale = 1f;
            });
        }

        public void CloseLottery()
        {
            foreach (LotteryResult lr in lotteryResults)
                lr.gameObject.SetActive(false);

            closeLottery.gameObject.SetActive(false);
            pnl_lottery.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        private Skill GetRandomSkill(ref List<Skill> shuffleBox)
        {
            var priority = shuffleBox.FindAll(s => s is ActiveSkill && s.Level == s.SkillInfo.MaxLevel - 1);
            var pool = priority.Count > 0 ? priority : shuffleBox;
            return pool[UnityEngine.Random.Range(0, pool.Count)];
        }
    }
}
