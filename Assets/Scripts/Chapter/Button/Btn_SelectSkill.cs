using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class Btn_SelectSkill : MonoBehaviour
    {
        public ChapterCtrl chapterCtrl;

        [SerializeField] Sprite[] sprite;
        [SerializeField] Image panelImage;
        [SerializeField] TextMeshProUGUI newTxt;

        [Space]
        public Skill skill;

        [Space]
        [SerializeField] Skill.SkillType skillType;
        [SerializeField] TextMeshProUGUI skillName;
        [SerializeField] Image skillImage;
        [SerializeField] TextMeshProUGUI upgradeInfo;

        private void OnEnable()
        {
            skillType = skill.Type;
            skillName.text = skill.SkillName;
            skillImage.sprite = skill.Sprite;
            upgradeInfo.text = skill.UpgradeInfos[skill.Level];

            panelImage.sprite = sprite[(int)skillType];
            if(skill.Level < 1)
                newTxt.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            if (newTxt.gameObject.activeSelf)
                newTxt.gameObject.SetActive(false);
        }

        public void Select()
        {
            chapterCtrl.SelectSkill(skill);
        }
    }
}