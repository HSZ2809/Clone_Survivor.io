using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class Btn_SelectSkill : MonoBehaviour
    {
        [SerializeField] ChapterCtrl chapterCtrl;

        [SerializeField] Sprite[] bgSprite;
        [SerializeField] Sprite[] starSprite;

        [Space]
        [SerializeField] Image panelImage;
        [SerializeField] TextMeshProUGUI newTxt;
        [SerializeField] Image[] stars;
        [SerializeField] Animator[] starsAnim;

        Skill skill;

        [Space]
        [SerializeField] SkillInformation.SkillType skillType;
        [SerializeField] TextMeshProUGUI skillName;
        [SerializeField] Image skillImage;
        [SerializeField] TextMeshProUGUI upgradeInfo;

        private void OnEnable()
        {
            int index;

            if (skill.Level >= 5)
                index = 2;
            else
                index = skill.Level;

            starsAnim[index].Play("StarBlink");
        }

        private void OnDisable()
        {
            int index;

            if (skill.Level >= 5)
                index = 2;
            else
                index = skill.Level;

            stars[index].color = Color.white;
        }

        public void SetSkill(Skill _skill)
        {
            ResetButton();

            skill = _skill;

            skillType = skill.SkillInfo.Type;
            skillName.text = skill.SkillInfo.SkillName;

            upgradeInfo.text = skill.SkillInfo.UpgradeInfos[skill.Level];

            int index = 0;
            if (skill.Level >= 5)
            {
                index = 2;
                stars[index].sprite = starSprite[2];
                skillImage.sprite = skill.SkillInfo.Sprite[1];
            }
            else
            {
                while (index < skill.Level)
                {
                    stars[index].sprite = starSprite[1];
                    index += 1;
                }
                stars[index].sprite = starSprite[1];
                skillImage.sprite = skill.SkillInfo.Sprite[0];
            }

            panelImage.sprite = bgSprite[(int)skillType];
            if (skill.Level < 1)
                newTxt.gameObject.SetActive(true);
        }

        public void ResetButton()
        {
            foreach (Image star in stars)
                star.sprite = starSprite[0];

            if (newTxt.gameObject.activeSelf)
                newTxt.gameObject.SetActive(false);
        }

        public void Select()
        {
            chapterCtrl.SelectSkill(skill);
        }
    }
}
