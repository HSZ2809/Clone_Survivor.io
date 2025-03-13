using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening

namespace ZUN
{
    public class Btn_SelectSkill : MonoBehaviour
    {
        public ChapterCtrl chapterCtrl;

        [SerializeField] Sprite[] sprite;
        [SerializeField] Image panelImage;
        [SerializeField] TextMeshProUGUI newTxt;
        [SerializeField] Image[] stars;

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
            int index = 0;
            while (index < skill.Level)
            {
                // stars[i]. 알파를 1로 변경
                stars[i].gameObject.SetActive(true);
                index += 1;
            }
            StartCoroutine(BlinkStar(stars[index]));

            panelImage.sprite = sprite[(int)skillType];
            if(skill.Level < 1)
                newTxt.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            if (newTxt.gameObject.activeSelf)
                newTxt.gameObject.SetActive(false);
        }

        IEnumerator BlinkStar(Image star)
        {
            star.DOFade(1.0f, 1.0f).SetLoops(2, LoopType.Yoyo).OnComplete(() => {
                StartCoroutine(BlinkStar(stars[index]))
            });

            yield return null;
        }

        public void Select()
        {
            chapterCtrl.SelectSkill(skill);
        }
    }
}
