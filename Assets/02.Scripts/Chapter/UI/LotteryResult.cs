using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class LotteryResult : MonoBehaviour
    {
        [SerializeField] Sprite[] bgSprite;
        [SerializeField] Sprite[] starSprite;

        [Space]
        [SerializeField] Image img_bg;
        [SerializeField] Image img_skill;
        [SerializeField] Image[] img_star;
        [SerializeField] TextMeshProUGUI txt_skillName;
        [SerializeField] TextMeshProUGUI txt_upgradeInfo;

        readonly int PASSIVE = 0, ACTIVE = 1, FINAL = 2;
        readonly int BG = 0, YELLOW = 1, RED = 2;

        public void SetData(Skill skill)
        {
            if (skill.SkillInfo.Type == SkillInformation.SkillType.PASSIVE)
            {
                img_bg.sprite = bgSprite[PASSIVE];
                img_skill.sprite = skill.SkillInfo.Sprite[0];
            }
            else
            {
                if (skill.Level == skill.SkillInfo.MaxLevel)
                {
                    img_bg.sprite = bgSprite[FINAL];
                    img_skill.sprite = skill.SkillInfo.Sprite[1];
                }
                else
                {
                    img_bg.sprite = bgSprite[ACTIVE];
                    img_skill.sprite = skill.SkillInfo.Sprite[0];
                }     
            }

            
            txt_skillName.text = skill.SkillInfo.SkillName;
            txt_upgradeInfo.text = skill.SkillInfo.UpgradeInfos[skill.Level - 1];

            if (skill.Level >= 6)
            {
                img_star[2].sprite = starSprite[RED];
                img_star[2].gameObject.SetActive(true);
            }
            else
            {
                for(int i = 0; i < img_star.Length; i++)
                {
                    if (i < skill.Level)
                        img_star[i].sprite = starSprite[YELLOW];
                    else
                        img_star[i].sprite = starSprite[BG];

                    img_star[i].gameObject.SetActive(true);
                }
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < img_star.Length; i++)
                img_star[i].gameObject.SetActive(false);
        }
    }
}
