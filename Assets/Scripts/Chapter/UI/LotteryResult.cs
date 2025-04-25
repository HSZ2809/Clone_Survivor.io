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

        readonly int PASSIVE = 0, ACTIVE = 1, FINALACT = 2;
        readonly int BG = 0, YELLOW = 1, RED = 2;

        public void SetData(Skill skill)
        {
            if (skill.SkillInfo.Type == SkillInformation.SkillType.PASSIVE)
                img_bg.sprite = bgSprite[PASSIVE];
            else
            {
                if (skill.SkillInfo.MaxLevel == 1)
                    img_bg.sprite = bgSprite[FINALACT];
                else
                    img_bg.sprite = bgSprite[ACTIVE];
            }

            img_skill.sprite = skill.SkillInfo.Sprite;
            txt_skillName.text = skill.SkillInfo.SkillName;
            txt_upgradeInfo.text = skill.SkillInfo.UpgradeInfos[skill.Level - 1];

            if (skill.SkillInfo.MaxLevel == 1)
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
