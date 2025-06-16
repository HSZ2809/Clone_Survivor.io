using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ZUN
{
    public class AlertPopup : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI tmp_message;

        readonly float scaleUpDuration = 0.3f;
        readonly float moveUpDuration = 0.3f;
        readonly float fadeoutDuration = 1.0f;
        readonly float moveDistance = 70f;

        private void Start()
        {
            ShowPopup();
        }

        public void SetMessage(string message)
        {
            tmp_message.text = message;
        }

        void ShowPopup()
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            transform.DOScale(new Vector3(1f, 1f, 1f), scaleUpDuration);
            transform.DOMove(transform.position + Vector3.up * moveDistance, moveUpDuration)
                        .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    tmp_message.DOFade(0f, fadeoutDuration).From(1f)
                        .SetEase(Ease.Linear)
                        .OnComplete(() =>
                        {
                            Destroy(gameObject);
                        });
                });
        }
    }
}