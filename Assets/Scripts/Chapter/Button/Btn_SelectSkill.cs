using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

        [Space]
        public Skill skill;

        [Space]
        [SerializeField] SkillInformation.SkillType skillType;
        [SerializeField] TextMeshProUGUI skillName;
        [SerializeField] Image skillImage;
        [SerializeField] TextMeshProUGUI upgradeInfo;

        private void OnEnable()
        {
            skillType = skill.SkillInfo.Type;
            skillName.text = skill.SkillInfo.SkillName;
            skillImage.sprite = skill.SkillInfo.Sprite;
            upgradeInfo.text = skill.SkillInfo.UpgradeInfos[skill.Level];

            int index = 0;
            while (index < skill.Level)
            {
                stars[index].sprite = starSprite[1];
                index += 1;
            }              
            stars[index].sprite = starSprite[1]; // 점멸 효과 추가 필요

            // StartCoroutine(BlinkStar(stars[skill.Level]));

            panelImage.sprite = bgSprite[(int)skillType];
            if (skill.Level < 1)
                newTxt.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            foreach (Image star in stars)
                star.sprite = starSprite[0];

            if (newTxt.gameObject.activeSelf)
                newTxt.gameObject.SetActive(false);
        }

        IEnumerator BlinkStar(Image star)
        {
            star.DOFade(1.0f, 1.0f).SetLoops(2, LoopType.Yoyo).OnComplete(() => {
                StartCoroutine(BlinkStar(star));
            });

            yield return null;
        }

        public void Select()
        {
            chapterCtrl.SelectSkill(skill);
        }
    }
}
