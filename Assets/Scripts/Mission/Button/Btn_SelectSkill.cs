using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class Btn_SelectSkill : MonoBehaviour
    {
        public MissionCtrl missionCtrl;

        [SerializeField] Sprite[] sprite;
        [SerializeField] Image panelImage;

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
        }

        public void Select()
        {
            missionCtrl.SelectSkill(skill);
        }
    }
}