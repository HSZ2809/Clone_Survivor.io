using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace ZUN
{
    public class WarningSign : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] Image warningMark;
        [SerializeField] Image glow;
        [SerializeField] Image WarningLine;
        [SerializeField] TextMeshProUGUI warningText;
        [SerializeField] AudioSource warningAudioSource;

        [Header("Zombie Warn Value")]
        [SerializeField] int zombieTransitionTime;
        [SerializeField] string zombieString;

        [Header("Boss Warn Value")]
        [SerializeField] int bossTransitionTime;
        [SerializeField] string bossString;

        //public enum WarningType
        //{
        //    ZOMBIE, BOSS
        //}

        public void ZombieWarning()
        {
            warningText.text = zombieString;
            StartWarning(zombieTransitionTime);
        }

        public void BossWarning()
        {
            warningText.text = bossString;
            StartWarning(bossTransitionTime);
        }

        private void StartWarning(int totalTime)
        {
            // WarningType 별로 경고 문구, 시퀀스 시간 변경

            //int totalTime;
            //switch(type)
            //{
            //    case WarningType.ZOMBIE:
            //        totalTime = zombieTransitionTime;
            //        warningText.text = zombieString;
            //        break;
            //    case WarningType.BOSS:
            //        totalTime = bossTransitionTime;
            //        warningText.text = bossString;
            //        break;
            //    default:
            //        Debug.LogWarning("WarningSign : Enexpected WarningType");
            //        return;
            //}

            int blinkSequenceRepeat = totalTime * 10;
            int pulseSequenceRepeat = (totalTime * 10) - 4;

            // WarningLine이 왼쪽 화면 밖으로 이동
            WarningLine.rectTransform.anchoredPosition = new Vector2(-Screen.width, WarningLine.rectTransform.anchoredPosition.y);

            // warningAudioSource의 클립 실행
            warningAudioSource.Play();

            // glow가 totalTime초 동안 깜빡이게 하는 효과
            Sequence blinkSequence = DOTween.Sequence();
            blinkSequence.Append(glow.DOFade(0.15f, 0.1f).SetLoops(blinkSequenceRepeat, LoopType.Yoyo));

            // WarningLine이 화면 밖에서 화면 중앙으로 이동 (0.2초)
            // warningText와 warningMark를 서서히 보이게 하고 튀어 나오는 효과 발생 (0.2초)
            Sequence showSequence = DOTween.Sequence();
            showSequence.Append(WarningLine.rectTransform.DOAnchorPosX(0, 0.2f).SetEase(Ease.Linear))
                        .Join(WarningLine.DOFade(1, 0.2f).From(0))
                        .Join(warningText.DOFade(1, 0.2f).From(0))
                        .Join(warningMark.DOFade(1, 0.2f).From(0))
                        .Join(warningText.rectTransform.DOScale(1.5f, 0.2f).SetLoops(2, LoopType.Yoyo))
                        .Join(warningMark.rectTransform.DOScale(1.5f, 0.2f).SetLoops(2, LoopType.Yoyo));

            // warningText와 WarningLine이 켜지다 작아지는 것을 (totalTime - 0.2 - 0.2)초 동안 반복
            Sequence pulseSequence = DOTween.Sequence();
            pulseSequence.Append(warningText.rectTransform.DOScale(1.2f, 0.1f).SetLoops(pulseSequenceRepeat, LoopType.Yoyo))
                         .Join(WarningLine.rectTransform.DOScale(1.2f, 0.1f).SetLoops(pulseSequenceRepeat, LoopType.Yoyo));

            // 크기가 커지면서 서서히 투명해짐 (0.2초)
            Sequence fadeOutSequence = DOTween.Sequence();
            fadeOutSequence.Append(warningText.rectTransform.DOScale(1.5f, 0.2f))
                           .Join(warningMark.rectTransform.DOScale(1.5f, 0.2f))
                           .Join(WarningLine.rectTransform.DOScale(1.5f, 0.2f))
                           .Join(warningText.DOFade(0, 0.2f))
                           .Join(warningMark.DOFade(0, 0.2f))
                           .Join(WarningLine.DOFade(0, 0.2f));

            // 시퀀스 연결
            Sequence resultSequence = DOTween.Sequence();
            resultSequence.Append(showSequence)
                          .Append(pulseSequence)
                          .Append(fadeOutSequence);

            resultSequence.Insert(0f, blinkSequence);

            // 시퀀스 실행
            resultSequence.Play();
        }
    }
}