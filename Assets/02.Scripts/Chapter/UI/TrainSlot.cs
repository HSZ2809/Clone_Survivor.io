using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class TrainSlot : MonoBehaviour
    {
        [SerializeField] Image bgImage;
        [SerializeField] Image iconImage;
        [SerializeField] Image glowImage;

        [SerializeField] Sprite slotBG;
        [SerializeField] Sprite[] hightLighrtSprite;

        const int HIGHLIGHT_PASSIVE        = 0;
        const int HIGHLIGHT_ACTIVE_NORMAL  = 1;
        const int HIGHLIGHT_ACTIVE_ENHANCE = 2;
        Tween _glowTween;
        Sequence _bounceSeq;

        void Awake()
        {
            iconImage.raycastTarget = false;
            glowImage.raycastTarget = false;
            bgImage.raycastTarget = false;
        }

        public void SetIcon(Sprite icon)
        {
            iconImage.sprite = icon;
        }

        // 빛 즉시 켜기
        public void Activate()
        {
            _glowTween?.Kill();
            var c = glowImage.color;
            c.a = 1f;
            glowImage.color = c;
        }

        // 빛 서서히 끄기 (SetUpdate(true) → timeScale=0에서도 동작)
        public void Deactivate()
        {
            _glowTween?.Kill();
            _glowTween = glowImage.DOFade(0f, 0.15f).SetUpdate(true);
        }

        public void ResetGlow()
        {
            _glowTween?.Kill();
            _bounceSeq?.Kill();
            var c = glowImage.color;
            c.a = 0f;
            glowImage.color = c;
            GetComponent<RectTransform>().localScale = Vector3.one;

            if (slotBG != null)
                bgImage.sprite = slotBG;
        }

        public void SetHighlight(bool isPassive, bool isMaxLevel)
        {
            if (hightLighrtSprite == null) return;
            int idx = isPassive ? HIGHLIGHT_PASSIVE
                    : isMaxLevel ? HIGHLIGHT_ACTIVE_ENHANCE
                    : HIGHLIGHT_ACTIVE_NORMAL;
            if (idx < hightLighrtSprite.Length)
                bgImage.sprite = hightLighrtSprite[idx];
        }

        public void ActivateWinner(Color highlightColor)
        {
            _glowTween?.Kill();
            _bounceSeq?.Kill();

            var c = highlightColor;
            c.a = 1f;
            glowImage.color = c;

            var rt = GetComponent<RectTransform>();
            rt.localScale = Vector3.one;
            _bounceSeq = DOTween.Sequence().SetUpdate(true)
                .Append(rt.DOScale(1.2f, 0.12f))
                .Append(rt.DOScale(1.0f, 0.10f))
                .AppendInterval(0.05f)
                .Append(rt.DOScale(1.15f, 0.10f))
                .Append(rt.DOScale(1.0f, 0.10f))
                .AppendInterval(0.05f)
                .Append(rt.DOScale(1.1f, 0.08f))
                .Append(rt.DOScale(1.0f, 0.08f));
        }
    }
}
